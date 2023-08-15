// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow
{
	/// <summary>
	/// Runtime behavior tree graph view.
	/// </summary>
	public sealed class AgentBehaviorTreeGraph : GraphView, IDisposable
	{
		private const float DefaultSizeX = 150f;
		private const float DefaultSizeY = 150f;
		private const float DefaultPlaceX = DefaultSizeX + 150f;
		private const float DefaultPlaceY = DefaultSizeY + 50f;
		private const float OffsetX = 30f;
		private const float OffsetY = 50f;
		private const float MinimalCentralY = 300f;
		private static readonly Vector2 s_defaultSize = new Vector2(DefaultSizeX, DefaultSizeY);

		[NotNull] public readonly TreeRoot treeRoot;
		[NotNull] private readonly AgentBehaviorTreeNode[] m_nodes;

		/// <summary>
		/// Creates <see cref="AgentBehaviorTreeGraph"/>.
		/// </summary>
		/// <param name="behaviorInfos">Infos of all behaviors of <paramref name="treeRoot"/>.</param>
		/// <param name="treeRoot">Tree root.</param>
		public AgentBehaviorTreeGraph([NotNull] List<BehaviorInfo> behaviorInfos, [NotNull] TreeRoot treeRoot)
		{
			this.treeRoot = treeRoot;

			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

			var background = new GridBackground();
			Insert(0, background);
			background.StretchToParentSize();

			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());

			var minimap = new MiniMap {anchored = true};
			minimap.SetPosition(new Rect(10f, 10f, 200f, 200f));
			Add(minimap);

			var rootNode = new RootNode();
			AddElement(rootNode);

			int behaviorCount = behaviorInfos.Count;
			m_nodes = new AgentBehaviorTreeNode[behaviorCount];

			for (int i = 0; i < behaviorCount; ++i)
			{
				AgentBehaviorTreeNode node = m_nodes[i] = new AgentBehaviorTreeNode(behaviorInfos[i].behavior);
				AddElement(node);
			}

			for (int i = 0; i < behaviorCount; ++i)
			{
				SetChildren(behaviorInfos, i);
			}

			AddElement(rootNode.AddChild(m_nodes[0]));

			int levelCount = 0;
			for (int i = 0; i < behaviorCount; ++i)
			{
				levelCount = Mathf.Max(levelCount, behaviorInfos[i].level);
			}
			++levelCount;

			int[] levelCounts = new int[levelCount];

			for (int i = 0, count = behaviorInfos.Count; i < count; ++i)
			{
				++levelCounts[behaviorInfos[i].level];
			}

			int biggestLevelCount = 0;

			for (int i = 0, count = levelCounts.Length; i < count; ++i)
			{
				biggestLevelCount = Mathf.Max(biggestLevelCount, levelCounts[i]);
			}

			float centralY = biggestLevelCount * DefaultPlaceY / 2f;
			centralY = Mathf.Max(centralY, MinimalCentralY);

			rootNode.SetPosition(new Rect(
				new Vector2(OffsetX, centralY - DefaultPlaceY / 2f + OffsetY), s_defaultSize));

			int[] setLevelNodes = new int[levelCount];

			for (int i = 0; i < behaviorCount; ++i)
			{
				int level = behaviorInfos[i].level;
				float positionX = DefaultPlaceX * (level + 1) + OffsetX;
				float levelCentralY = levelCounts[level] * DefaultPlaceY / 2f;
				float levelCentralYDiff = centralY - levelCentralY;
				float positionY = setLevelNodes[level] * DefaultPlaceY + levelCentralYDiff + OffsetY;
				m_nodes[i].SetPosition(new Rect(new Vector2(positionX, positionY), s_defaultSize));
				++setLevelNodes[level];
			}

			for (int i = 0, count = m_nodes.Length; i < count; ++i)
			{
				AgentBehaviorTreeNode node = m_nodes[i];
				node.RefreshExpandedState();
				node.RefreshPorts();
			}

			rootNode.RefreshExpandedState();
			rootNode.RefreshPorts();

			graphViewChanged += change =>
			{
				change.elementsToRemove?.Clear();
				change.edgesToCreate?.Clear();
				return change;
			};
			EditorApplication.update += Update;
		}

		public void Dispose()
		{
			EditorApplication.update -= Update;
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		{
		}

		private void SetChildren([NotNull] List<BehaviorInfo> behaviorInfos, int index)
		{
			AgentBehaviorTreeNode node = m_nodes[index];
			int level = behaviorInfos[index].level;

			for (int i = index + 1, count = behaviorInfos.Count; i < count; ++i)
			{
				int childLevel = behaviorInfos[i].level;

				if (childLevel <= level)
				{
					return;
				}

				if (childLevel - level != 1)
				{
					continue;
				}

				AgentBehaviorTreeNode childNode = m_nodes[i];
				Edge edge = node.AddChild(childNode);
				AddElement(edge);
			}
		}

		private void Update()
		{
			for (int i = 0, count = m_nodes.Length; i < count; ++i)
			{
				m_nodes[i].Update();
			}
		}
	}
}
