// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Serialization;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;
using Object = UnityEngine.Object;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SerializedBehaviorTreeGraph : GraphView, IDisposable
	{
		[NotNull] private const string SerializedBehaviorDataPropertyName = "m_SerializedBehaviorData";
		[NotNull] private const string RootNodePropertyName = "m_RootNode";
		[NotNull] private const string RootGraphInfoPropertyName = "m_RootGraphInfo";

		[NotNull] private const string SerializedBehaviorPropertyName = "serializedBehavior";
		[NotNull] private const string ChildrenIndicesPropertyName = "childrenIndices";
		[NotNull] private const string NodeGraphInfoPropertyName = "nodeGraphInfo";

		[NotNull] private const string PositionPropertyName = "position";

		private static readonly Vector2 s_defaultSize = new Vector2(200f, 200f);

		[NotNull] private readonly SerializedBehaviorTree m_serializedBehaviorTree;
		[NotNull] private readonly RootNode m_rootNode;
		[NotNull, ItemNotNull] private readonly List<SerializedBehaviorTreeNode> m_nodes =
			new List<SerializedBehaviorTreeNode>();

		[NotNull] private readonly EditorWindow m_editorWindow;
		[NotNull] private readonly SearchWindowProvider m_searchWindowProvider;

		public SerializedBehaviorTreeGraph([NotNull] SerializedBehaviorTree serializedBehaviorTree,
			[NotNull] EditorWindow editorWindow)
		{
			m_serializedBehaviorTree = serializedBehaviorTree;
			m_editorWindow = editorWindow;
			m_searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();
			m_searchWindowProvider.Initialize(this);

			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());

			var background = new GridBackground();
			Insert(0, background);
			background.StretchToParentSize();

			var minimap = new MiniMap {anchored = true};
			minimap.SetPosition(new Rect(10f, 10f, 200f, 200f));
			Add(minimap);

			m_rootNode = new RootNode();
			AddElement(m_rootNode);

			Update();

			graphViewChanged += OnGraphViewChanged;
			nodeCreationRequest += OnCreateNode;
			m_serializedBehaviorTree.OnAssetChanged += Update;
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

		public void OnPortAdded([NotNull] SerializedBehaviorTreeNode node)
		{
			var serializedBehaviorTree = new SerializedObject(m_serializedBehaviorTree);
			SerializedProperty serializedBehaviors = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");

			for (int i = 0, count = serializedBehaviors.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehavior = serializedBehaviors.GetArrayElementAtIndex(i);

				if (serializedBehavior.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
					node.dependedSerializedBehavior)
				{
					SerializedProperty childrenIndices = serializedBehavior.FindPropertyRelative("childrenIndices");
					++childrenIndices.arraySize;
					childrenIndices.GetArrayElementAtIndex(childrenIndices.arraySize - 1).intValue = -1;
				}
			}

			serializedBehaviorTree.ApplyModifiedProperties();
		}

		public void OnPortRemoved([NotNull] SerializedBehaviorTreeNode node, [CanBeNull] Edge edge, int index)
		{
			var serializedBehaviorTree = new SerializedObject(m_serializedBehaviorTree);
			SerializedProperty serializedBehaviors = serializedBehaviorTree.FindProperty("m_SerializedBehaviorData");

			for (int i = 0, count = serializedBehaviors.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehavior = serializedBehaviors.GetArrayElementAtIndex(i);

				if (serializedBehavior.FindPropertyRelative("serializedBehavior").objectReferenceValue ==
					node.dependedSerializedBehavior)
				{
					SerializedProperty childrenIndices = serializedBehavior.FindPropertyRelative("childrenIndices");
					childrenIndices.DeleteArrayElementAtIndex(index);
				}
			}

			serializedBehaviorTree.ApplyModifiedProperties();
		}

		public void CreateNewBehavior([NotNull] Type behaviorType, Vector2 position)
		{
			var behavior = (SerializedBehavior_Base)ScriptableObject.CreateInstance(behaviorType);
			behavior.name = behaviorType.Name;
			AssetDatabase.AddObjectToAsset(behavior, m_serializedBehaviorTree);
			var node = new SerializedBehaviorTreeNode(behavior, this);
			AddElement(node);
			m_nodes.Add(node);

			var serializedTree = new SerializedObject(m_serializedBehaviorTree);
			SerializedProperty serializedDatas = serializedTree.FindProperty("m_SerializedBehaviorData");
			int index = serializedDatas.arraySize++;
			SerializedProperty serializedData = serializedDatas.GetArrayElementAtIndex(index);
			serializedData.FindPropertyRelative("serializedBehavior").objectReferenceValue = behavior;
			position -= m_editorWindow.position.position;
			position = contentViewContainer.WorldToLocal(position);
			serializedData.FindPropertyRelative("nodeGraphInfo").FindPropertyRelative("position").vector2Value =
				position;
			node.SetPosition(new Rect(position, s_defaultSize));

			SerializedProperty children = serializedData.FindPropertyRelative("childrenIndices");
			if (behavior.serializedBehaviorType.IsSubclassOf(typeof(Composite)))
			{
				node.SetOutputCapacity(new List<Edge>(),2);
				children.arraySize = 2;
				children.GetArrayElementAtIndex(0).intValue = -1;
				children.GetArrayElementAtIndex(1).intValue = -1;
			}
			else if (behavior.serializedBehaviorType.IsSubclassOf(typeof(Decorator)))
			{
				node.SetOutputCapacity(new List<Edge>(), 1);
				children.arraySize = 1;
				children.GetArrayElementAtIndex(0).intValue = -1;
			}
			else
			{
				node.SetOutputCapacity(new List<Edge>(), 0);
				children.arraySize = 0;
			}

			node.RefreshExpandedState();
			node.RefreshPorts();

			serializedTree.ApplyModifiedProperties();
		}

		public void Dispose()
		{
			Object.DestroyImmediate(m_searchWindowProvider);
			m_serializedBehaviorTree.OnAssetChanged -= Update;
		}

		private void Update()
		{
			var serializedBehaviorTree = new SerializedObject(m_serializedBehaviorTree);
			SerializedProperty serializedBehaviorDataArray =
				serializedBehaviorTree.FindProperty(SerializedBehaviorDataPropertyName);

			UpdateDeletedBehaviors(serializedBehaviorDataArray);
			UpdateCreatedBehaviors(serializedBehaviorDataArray);
			UpdatePositions(serializedBehaviorDataArray);
			UpdateChildren(serializedBehaviorDataArray);
			UpdateRoot(serializedBehaviorTree);
			RefreshNodes();
		}

		private void UpdateDeletedBehaviors([NotNull] SerializedProperty serializedBehaviorDataArray)
		{
			for (int i = m_nodes.Count - 1; i >= 0; --i)
			{
				SerializedBehaviorTreeNode node = m_nodes[i];

				bool exists = false;

				for (int j = 0, count = serializedBehaviorDataArray.arraySize; j < count & !exists; ++j)
				{
					exists = serializedBehaviorDataArray.GetArrayElementAtIndex(j)
							.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue ==
						node.dependedSerializedBehavior;
				}

				if (!exists)
				{
					RemoveConnections(node);
					RemoveElement(node);
					m_nodes.RemoveAt(i);
				}
			}
		}

		private void UpdateCreatedBehaviors([NotNull] SerializedProperty serializedBehaviorDataArray)
		{
			for (int i = 0, count = serializedBehaviorDataArray.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehaviorData = serializedBehaviorDataArray.GetArrayElementAtIndex(i);
				var serializedBehavior = (SerializedBehavior_Base)serializedBehaviorData
					.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue;

				if (FindNode(serializedBehavior) != null)
				{
					continue;
				}

				var newNode = new SerializedBehaviorTreeNode(serializedBehavior, this);
				AddElement(newNode);
				m_nodes.Add(newNode);
			}
		}

		private void UpdatePositions([NotNull] SerializedProperty serializedBehaviorDataArray)
		{
			for (int i = 0, count = serializedBehaviorDataArray.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehaviorData = serializedBehaviorDataArray.GetArrayElementAtIndex(i);
				var serializedBehavior = (SerializedBehavior_Base)serializedBehaviorData
					.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue;
				SerializedBehaviorTreeNode node = FindNode(serializedBehavior);
				Vector2 position = serializedBehaviorData.FindPropertyRelative(NodeGraphInfoPropertyName)
					.FindPropertyRelative(PositionPropertyName).vector2Value;
				node.SetPosition(new Rect(position, s_defaultSize));
			}
		}

		private void RemoveConnections([NotNull] SerializedBehaviorTreeNode node)
		{
			for (int edgeIndex = 0, edgeCount = node.outputEdgeCount; edgeIndex < edgeCount; ++edgeIndex)
			{
				Edge edge = node.RemoveChild(edgeIndex);

				if (edge != null)
				{
					RemoveElement(edge);
				}
			}

			for (int nodeIndex = 0, nodeCount = m_nodes.Count; nodeIndex < nodeCount; ++nodeIndex)
			{
				SerializedBehaviorTreeNode otherNode = m_nodes[nodeIndex];

				for (int childIndex = 0, childrenCount = otherNode.outputEdgeCount;
					childIndex < childrenCount;
					++childIndex)
				{
					if (otherNode.GetChild(childIndex) == node)
					{
						Edge edge = otherNode.RemoveChild(childIndex);

						if (edge != null)
						{
							RemoveElement(edge);
						}
					}
				}
			}
		}

		private void UpdateChildren([NotNull] SerializedProperty serializedBehaviorDataArray)
		{
			var removedEdges = new List<Edge>();

			for (int i = 0, count = serializedBehaviorDataArray.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehaviorData = serializedBehaviorDataArray.GetArrayElementAtIndex(i);
				var serializedBehavior = (SerializedBehavior_Base)serializedBehaviorData
					.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue;
				SerializedBehaviorTreeNode node = FindNode(serializedBehavior);
				SerializedProperty children = serializedBehaviorData.FindPropertyRelative(ChildrenIndicesPropertyName);
				node.SetOutputCapacity(removedEdges, children.arraySize);

				for (int edgeIndex = 0, edgeCount = removedEdges.Count; edgeIndex < edgeCount; ++edgeIndex)
				{
					RemoveElement(removedEdges[edgeIndex]);
				}

				removedEdges.Clear();

				for (int childIndex = 0, childrenCount = children.arraySize; childIndex < childrenCount; ++childIndex)
				{
					int childBehaviorIndex = children.GetArrayElementAtIndex(childIndex).intValue;

					if (childBehaviorIndex < 0)
					{
						if (node.GetChild(childIndex) != null)
						{
							Edge edge = node.RemoveChild(childIndex);
							RemoveElement(edge);
						}

						continue;
					}

					var child = (SerializedBehavior_Base)serializedBehaviorDataArray
						.GetArrayElementAtIndex(childBehaviorIndex)
						.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue;

					if (node.GetChild(childIndex)?.dependedSerializedBehavior == child)
					{
						continue;
					}

					Edge previousEdge = node.RemoveChild(childIndex);

					if (previousEdge != null)
					{
						RemoveElement(previousEdge);
					}

					AddElement(node.SetChild(FindNode(child), childIndex));
				}
			}
		}

		private void UpdateRoot([NotNull] SerializedObject serializedTree)
		{
			int rootNode = serializedTree.FindProperty(RootNodePropertyName).intValue;
			Vector2 position = serializedTree.FindProperty(RootGraphInfoPropertyName)
				.FindPropertyRelative(PositionPropertyName).vector2Value;

			if (rootNode < 0)
			{
				Edge edge = m_rootNode.Disconnect();

				if (edge != null)
				{
					RemoveElement(edge);
				}
			}
			else
			{
				var rootBehavior = (SerializedBehavior_Base)serializedTree
					.FindProperty(SerializedBehaviorDataPropertyName)
					.GetArrayElementAtIndex(rootNode).FindPropertyRelative(SerializedBehaviorPropertyName)
					.objectReferenceValue;
				SerializedBehaviorTreeNode node = FindNode(rootBehavior);

				if (m_rootNode.GetChild() != node)
				{
					Edge edge = m_rootNode.Disconnect();

					if (edge != null)
					{
						RemoveElement(edge);
					}

					AddElement(m_rootNode.Connect(node));
				}
			}

			m_rootNode.SetPosition(new Rect(position, s_defaultSize));
		}

		private void RefreshNodes()
		{
			for (int i = 0, count = m_nodes.Count; i < count; ++i)
			{
				SerializedBehaviorTreeNode node = m_nodes[i];
				node.RefreshExpandedState();
				node.RefreshPorts();
			}

			m_rootNode.RefreshExpandedState();
			m_rootNode.RefreshPorts();
		}

		private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
		{
			var serializedBehaviorTree = new SerializedObject(m_serializedBehaviorTree);
			List<GraphElement> elementsToRemove = graphViewChange.elementsToRemove ?? new List<GraphElement>(0);
			elementsToRemove.Remove(m_rootNode);

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

				Undo.DestroyObjectImmediate(datas.GetArrayElementAtIndex(nodeToRemoveIndex).FindPropertyRelative("serializedBehavior").objectReferenceValue);

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

				m_nodes.Remove(nodeToRemove);
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
		private SerializedBehaviorTreeNode CreateNode([NotNull] SerializedBehavior_Base serializedBehavior,
			Vector2 position)
		{
			var node = new SerializedBehaviorTreeNode(serializedBehavior, this);
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

		private void OnCreateNode(NodeCreationContext context)
		{
			SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), m_searchWindowProvider);
		}
	}
}
