// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Serialization;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SerializedBehaviorTreeGraph : GraphView
	{
		private static readonly Vector2 s_defaultSize = new Vector2(200f, 200f);

		[NotNull] private readonly SerializedBehaviorTree m_serializedBehaviorTree;
		[NotNull] private readonly RootNode m_entry;
		[NotNull] private readonly List<SerializedBehaviorTreeNode> m_nodes = new List<SerializedBehaviorTreeNode>();

		public SerializedBehaviorTreeGraph([NotNull] SerializedBehaviorTree serializedBehaviorTree)
		{
			m_serializedBehaviorTree = serializedBehaviorTree;

			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());

			var background = new GridBackground();
			Insert(0, background);
			background.StretchToParentSize();

			var serializedObject = new SerializedObject(serializedBehaviorTree);

			m_entry = CreateEntryPoint(serializedObject);
			AddElement(m_entry);

			Parse(serializedObject);
		}

		[NotNull]
		private static RootNode CreateEntryPoint(SerializedObject serializedBehaviorTree)
		{
			var entry = new RootNode();

			Vector2 position = serializedBehaviorTree.FindProperty("m_RootGraphInfo")
				.FindPropertyRelative("position").vector2Value;
			entry.SetPosition(new Rect(position, s_defaultSize));

			entry.RefreshExpandedState();
			entry.RefreshPorts();

			return entry;
		}

		private void Parse([NotNull] SerializedObject serializedBehaviorTree)
		{
			SerializedProperty serializedBehaviors = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");

			for (int i = 0, count = serializedBehaviors.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehaviorData = serializedBehaviors.GetArrayElementAtIndex(i);
				var serializedBehavior = (SerializedBehavior_Base)serializedBehaviorData
					.FindPropertyRelative("serializedBehavior").objectReferenceValue;
				Vector2 position = serializedBehaviorData.FindPropertyRelative("nodeGraphInfo")
					.FindPropertyRelative("position").vector2Value;
				SerializedBehaviorTreeNode serializedBehaviorNode = CreateNode(serializedBehavior, position);
				m_nodes.Add(serializedBehaviorNode);
				AddElement(serializedBehaviorNode);
			}

			for (int i = 0, count = serializedBehaviors.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehaviorData = serializedBehaviors.GetArrayElementAtIndex(i);
				SerializedProperty serializedChildren = serializedBehaviorData.FindPropertyRelative("childrenIndices");
				SerializedBehaviorTreeNode node = m_nodes[i];
				int arraySize = serializedChildren.arraySize;
				node.SetOutputCapacity(arraySize);

				for (int j = 0; j < arraySize; ++j)
				{
					int childIndex = serializedChildren.GetArrayElementAtIndex(j).intValue;
					var child = (SerializedBehavior_Base)serializedBehaviors.GetArrayElementAtIndex(childIndex)
						.FindPropertyRelative("serializedBehavior").objectReferenceValue;
					SerializedBehaviorTreeNode childNode = FindNode(child);
					Edge edge = node.SetChild(childNode, j);
					AddElement(edge);
				}
			}

			int rootNode = serializedBehaviorTree.FindProperty("m_RootNode").intValue;
			var outputPort = (Port)m_entry.outputContainer[0];
			var inputPort = (Port)m_nodes[rootNode].inputContainer[0];
			AddElement(outputPort.ConnectTo(inputPort));
		}

		[NotNull]
		private static SerializedBehaviorTreeNode CreateNode([NotNull] SerializedBehavior_Base serializedBehavior,
			Vector2 position)
		{
			var node = new SerializedBehaviorTreeNode(serializedBehavior);
			node.SetPosition(new Rect(position, s_defaultSize));

			node.RefreshExpandedState();
			node.RefreshPorts();

			return node;
		}

		[CanBeNull]
		private SerializedBehaviorTreeNode FindNode([NotNull] SerializedBehavior_Base serializedBehavior)
		{
			for (int i = 0, count = m_nodes.Count; i < count; ++i)
			{
				SerializedBehaviorTreeNode node = m_nodes[i];

				if (node.dependedSerializedBehavior == serializedBehavior)
				{
					return node;
				}
			}

			return null;
		}
	}
}
