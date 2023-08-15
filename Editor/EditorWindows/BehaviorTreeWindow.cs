// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Components;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow;
using Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow;
using Zor.BehaviorTree.Serialization;
using Object = UnityEngine.Object;

namespace Zor.BehaviorTree.EditorWindows
{
	/// <summary>
	/// <para>
	/// Behavior tree view window. It works for both serialized and runtime behavior trees.
	/// </para>
	/// <para>
	/// The window uses <see cref="Selection.activeObject"/> to select a behavior tree. If the selected object has
	/// multiple behavior trees, the window draws a selector.
	/// </para>
	/// </summary>
	public sealed class BehaviorTreeWindow : EditorWindow
	{
		private const string TreeRootFieldName = "m_treeRoot";
		private const string RootBehaviorFieldName = "m_rootBehavior";
		private const string CompositeChildrenFieldName = "children";
		private const string DecoratorChildFieldName = "child";

		private static readonly FieldInfo s_treeRootField = typeof(BehaviorTreeAgent)
			.GetField(TreeRootFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		private static readonly FieldInfo s_rootBehaviorField = typeof(TreeRoot)
			.GetField(RootBehaviorFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		private static readonly FieldInfo s_compositeChildrenField = typeof(Composite)
			.GetField(CompositeChildrenFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		private static readonly FieldInfo s_decoratorChildField = typeof(Decorator)
			.GetField(DecoratorChildFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		private SerializedBehaviorTreeGraph m_serializedGraph;
		private AgentBehaviorTreeGraph m_agentGraph;

		private Object m_selectedObject;
		private BehaviorTreeAgent[] m_selectedAgents;
		private int m_selectedIndex;

		[MenuItem("Window/Behavior Tree/Behavior Tree Window", priority = 2021)]
		private static void OpenWindow()
		{
			GetWindow<BehaviorTreeWindow>("Behavior Tree Window");
		}

		[OnOpenAsset(0)]
		private static bool OnOpenAsset(int instanceID, int line)
		{
			string path = AssetDatabase.GetAssetPath(instanceID);

			if (AssetDatabase.GetMainAssetTypeAtPath(path) != typeof(SerializedBehaviorTree))
			{
				return false;
			}

			OpenWindow();

			return true;
		}

		private void OnDestroy()
		{
			Clear();
		}

		private void CreateGUI()
		{
			CreateUI();
		}

		private void OnSelectionChange()
		{
			CreateUI();
		}

		private void OnFocus()
		{
			CreateUI();
		}

		private void CreateUI()
		{
			switch (Selection.activeObject)
			{
				case SerializedBehaviorTree serializedBehaviorTree:
					CreateSerializedBehaviorTree(serializedBehaviorTree);
					break;
				case GameObject gameObject when gameObject.scene.IsValid():
					CreateAgentBehaviorTree(gameObject);
					break;
				default:
					Clear();
					break;
			}
		}

		private void CreateSerializedBehaviorTree([NotNull] SerializedBehaviorTree serializedBehaviorTree)
		{
			if (m_selectedObject == serializedBehaviorTree)
			{
				return;
			}

			Clear();

			m_serializedGraph = new SerializedBehaviorTreeGraph(serializedBehaviorTree, this)
			{
				name = "Serialized Behavior Tree"
			};
			m_serializedGraph.style.flexGrow = 1f;
			rootVisualElement.Add(m_serializedGraph);

			m_selectedObject = serializedBehaviorTree;
		}

		private void CreateAgentBehaviorTree([NotNull] GameObject gameObject)
		{
			BehaviorTreeAgent[] agents = gameObject.GetComponents<BehaviorTreeAgent>();

			if (agents.Length == 0)
			{
				Clear();
				return;
			}

			if (m_selectedObject == gameObject &&
				m_selectedAgents != null && m_selectedAgents.Length == agents.Length)
			{
				bool same = true;

				for (int i = 0, count = m_selectedAgents.Length; i < count & same; ++i)
				{
					same = m_selectedAgents[i] == agents[i];
				}

				if (same && m_agentGraph != null &&
					s_treeRootField.GetValue(m_selectedAgents[m_selectedIndex]) == m_agentGraph.treeRoot)
				{
					return;
				}
			}

			Clear();

			VisualElement root = rootVisualElement;

			var toolbar = new Toolbar();
			root.Add(toolbar);

			var toolbarMenu = new ToolbarMenu();
			toolbar.Add(toolbarMenu);

			for (int i = 0, count = agents.Length; i < count; ++i)
			{
				int index = i;

				toolbarMenu.menu.AppendAction($"Behavior Tree Agent {index}",
					a =>
					{
						CreateAgentBehaviorTree((BehaviorTreeAgent)a.userData);
						toolbarMenu.text = $"Behavior Tree Agent {index}";
						m_selectedIndex = index;
					},
					a => DropdownMenuAction.Status.Normal,
					agents[i]);
			}

			CreateAgentBehaviorTree(agents[0]);
			toolbarMenu.text = "Behavior Tree Agent 0";
			m_selectedIndex = 0;

			m_selectedObject = gameObject;
			m_selectedAgents = agents;
		}

		private void CreateAgentBehaviorTree([NotNull] BehaviorTreeAgent agent)
		{
			if (m_agentGraph != null)
			{
				rootVisualElement.Remove(m_agentGraph);
				m_agentGraph = null;
			}

			if (!(s_treeRootField.GetValue(agent) is TreeRoot treeRoot))
			{
				return;
			}

			var rootBehavior = (Behavior)s_rootBehaviorField.GetValue(treeRoot);
			var behaviorInfos = new List<BehaviorInfo>();
			AddBehavior(behaviorInfos, rootBehavior, 0);

			m_agentGraph = new AgentBehaviorTreeGraph(behaviorInfos, treeRoot);
			m_agentGraph.style.flexGrow = 1f;
			rootVisualElement.Add(m_agentGraph);
		}

		private static void AddBehavior([NotNull] List<BehaviorInfo> behaviorInfos,
			[NotNull] Behavior behavior, int level)
		{
			var behaviorInfo = new BehaviorInfo {behavior = behavior, level = level};
			behaviorInfos.Add(behaviorInfo);

			Type behaviorType = behavior.GetType();

			if (behaviorType.IsSubclassOf(typeof(Composite)))
			{
				var children = (Behavior[])s_compositeChildrenField.GetValue(behavior);

				for (int i = 0, count = children.Length; i < count; ++i)
				{
					AddBehavior(behaviorInfos, children[i], level + 1);
				}
			}
			else if (behaviorType.IsSubclassOf(typeof(Decorator)))
			{
				var child = (Behavior)s_decoratorChildField.GetValue(behavior);
				AddBehavior(behaviorInfos, child, level + 1);
			}
		}

		private void Clear()
		{
			m_serializedGraph?.Dispose();
			m_serializedGraph = null;

			m_agentGraph?.Dispose();
			m_agentGraph = null;

			m_selectedObject = null;
			m_selectedAgents = null;

			rootVisualElement.Clear();
		}
	}
}
