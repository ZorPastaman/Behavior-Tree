// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections.Generic;
using System.Linq;
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

			graphViewChanged += OnGraphViewChanged;
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
		{
			var answer = new List<Port>();

			ports.ForEach(port =>
			{
				if (startPort != port && startPort.node != port.node)
				{
					answer.Add(port);
				}
			});

			return answer;
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

				node.RefreshExpandedState();
				node.RefreshPorts();

				for (int j = 0; j < arraySize; ++j)
				{
					int childIndex = serializedChildren.GetArrayElementAtIndex(j).intValue;

					if (childIndex < 0)
					{
						continue;
					}

					var child = (SerializedBehavior_Base)serializedBehaviors.GetArrayElementAtIndex(childIndex)
						.FindPropertyRelative("serializedBehavior").objectReferenceValue;
					SerializedBehaviorTreeNode childNode = FindNode(child);
					Edge edge = node.SetChild(childNode, j);
					AddElement(edge);
				}
			}

			int rootNode = serializedBehaviorTree.FindProperty("m_RootNode").intValue;
			if (rootNode >= 0)
			{
				var outputPort = (Port)m_entry.outputContainer[0];
				var inputPort = (Port)m_nodes[rootNode].inputContainer[0];
				AddElement(outputPort.ConnectTo(inputPort));
			}
		}

		private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
		{
			var serializedBehaviorTree = new SerializedObject(m_serializedBehaviorTree);
			List<GraphElement> elementsToRemove = graphViewChange.elementsToRemove ?? new List<GraphElement>(0);
			elementsToRemove.Remove(m_entry);

			for (int i = 0, count = elementsToRemove.Count; i < count; ++i)
			{
				RemoveElement(elementsToRemove[i]);
			}

			Edge[] edgesToRemove = elementsToRemove.OfType<Edge>().ToArray();
			for (int i = 0, count = edgesToRemove.Length; i < count; ++i)
			{
				Edge edgeToRemove = edgesToRemove[i];
				Node edgeNode = edgeToRemove.output.node;
				int edgeIndex = edgeNode.outputContainer.IndexOf(edgeToRemove.output);

				switch (edgeNode)
				{
					case SerializedBehaviorTreeNode treeNode:
						SerializedProperty datas = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");
						for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
						{
							SerializedProperty data = datas.GetArrayElementAtIndex(dataIndex);
							if (data.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
								treeNode.dependedSerializedBehavior)
							{
								data.FindPropertyRelative("childrenIndices").GetArrayElementAtIndex(edgeIndex).intValue = -1;
								break;
							}
						}
						break;
					case RootNode _:
						serializedBehaviorTree.FindProperty("m_RootNode").intValue = -1;
						break;
				}
			}

			SerializedBehaviorTreeNode[] nodesToRemove = elementsToRemove.OfType<SerializedBehaviorTreeNode>().ToArray();

			for (int i = 0, count = nodesToRemove.Length; i < count; ++i)
			{
				SerializedBehaviorTreeNode nodeToRemove = nodesToRemove[i];
				SerializedProperty datas = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");
				int nodeToRemoveIndex = -1;
				for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
				{
					SerializedProperty data = datas.GetArrayElementAtIndex(dataIndex);
					if (data.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
						nodeToRemove.dependedSerializedBehavior)
					{
						nodeToRemoveIndex = dataIndex;
						break;
					}
				}

				if (nodeToRemoveIndex < 0)
				{
					continue;
				}

				for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
				{
					SerializedProperty children = datas.GetArrayElementAtIndex(dataIndex)
						.FindPropertyRelative("childrenIndices");
					for (int childIndex = 0, childrenCount = children.arraySize; childIndex < childrenCount; ++childIndex)
					{
						SerializedProperty child = children.GetArrayElementAtIndex(childIndex);

						if (child.intValue > nodeToRemoveIndex)
						{
							--child.intValue;
						}
					}
				}

				Object.DestroyImmediate(datas.GetArrayElementAtIndex(nodeToRemoveIndex).FindPropertyRelative("serializedBehavior").objectReferenceValue, true);

				int datasSize = datas.arraySize;
				do
				{
					datas.DeleteArrayElementAtIndex(nodeToRemoveIndex);
				} while (datas.arraySize == datasSize);

				SerializedProperty rootNode = serializedBehaviorTree.FindProperty("m_RootNode");
				if (rootNode.intValue > nodeToRemoveIndex)
				{
					--rootNode.intValue;
				}
			}

			List<Edge> edgesToCreate = graphViewChange.edgesToCreate ?? new List<Edge>(0);
			for (int i = 0, count = edgesToCreate.Count; i < count; ++i)
			{
				Edge edgeToCreate = edgesToCreate[i];
				Node edgeNode = edgeToCreate.output.node;
				SerializedBehaviorTreeNode childNode = (SerializedBehaviorTreeNode)edgeToCreate.input.node;
				int edgeIndex = edgeNode.outputContainer.IndexOf(edgeToCreate.output);
				int childIndex = -1;

				SerializedProperty datas = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");
				for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
				{
					SerializedProperty data = datas.GetArrayElementAtIndex(dataIndex);
					if (data.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
						childNode.dependedSerializedBehavior)
					{
						childIndex = dataIndex;
						break;
					}
				}

				if (childIndex < 0)
				{
					continue;
				}

				switch (edgeNode)
				{
					case SerializedBehaviorTreeNode treeNode:
						for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
						{
							SerializedProperty data = datas.GetArrayElementAtIndex(dataIndex);
							if (data.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
								treeNode.dependedSerializedBehavior)
							{
								data.FindPropertyRelative("childrenIndices").GetArrayElementAtIndex(edgeIndex).intValue = childIndex;
								break;
							}
						}
						break;
					case RootNode _:
						serializedBehaviorTree.FindProperty("m_RootNode").intValue = childIndex;
						break;
				}
			}

			List<GraphElement> movedElements = graphViewChange.movedElements ?? new List<GraphElement>(0);
			for (int i = 0, count = movedElements.Count; i < count; ++i)
			{
				GraphElement movedElement = movedElements[i];

				switch (movedElement)
				{
					case SerializedBehaviorTreeNode treeNode:
						SerializedProperty datas = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");
						for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
						{
							SerializedProperty data = datas.GetArrayElementAtIndex(dataIndex);
							if (data.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
								treeNode.dependedSerializedBehavior)
							{
								data.FindPropertyRelative("nodeGraphInfo").FindPropertyRelative("position").vector2Value
									+= graphViewChange.moveDelta;
								break;
							}
						}
						break;
					case RootNode _:
						serializedBehaviorTree.FindProperty("m_RootGraphInfo").FindPropertyRelative("position")
							.vector2Value += graphViewChange.moveDelta;
						break;
				}
			}

			serializedBehaviorTree.ApplyModifiedProperties();
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			return graphViewChange;
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
