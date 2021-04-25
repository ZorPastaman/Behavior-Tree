// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
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
		[NotNull] private readonly SerializedObject m_serializedBehaviorTreeObject;
		[NotNull] private readonly RootNode m_rootNode;
		[NotNull, ItemNotNull] private readonly List<SerializedBehaviorTreeNode> m_nodes =
			new List<SerializedBehaviorTreeNode>();

		[NotNull] private readonly EditorWindow m_editorWindow;
		[NotNull] private readonly SearchWindowProvider m_searchWindowProvider;

		public SerializedBehaviorTreeGraph([NotNull] SerializedBehaviorTree serializedBehaviorTree,
			[NotNull] EditorWindow editorWindow)
		{
			m_serializedBehaviorTree = serializedBehaviorTree;
			m_serializedBehaviorTreeObject = new SerializedObject(serializedBehaviorTree);
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
			Undo.SetCurrentGroupName("Behavior Tree changed");
			int undoGroup = Undo.GetCurrentGroup();
			Undo.RegisterCompleteObjectUndo(m_serializedBehaviorTree, "Changed Behavior Tree");

			SerializedProperty serializedBehaviorData = GetPropertyData(node);
			SerializedProperty childrenIndices = serializedBehaviorData
				.FindPropertyRelative(ChildrenIndicesPropertyName);
			int last = childrenIndices.arraySize++;
			childrenIndices.GetArrayElementAtIndex(last).intValue = -1;

			m_serializedBehaviorTreeObject.ApplyModifiedProperties();

			Undo.CollapseUndoOperations(undoGroup);
		}

		public void OnPortRemoved([NotNull] SerializedBehaviorTreeNode node, [CanBeNull] Edge edge, int index)
		{
			Undo.SetCurrentGroupName("Behavior Tree changed");
			int undoGroup = Undo.GetCurrentGroup();
			Undo.RegisterCompleteObjectUndo(m_serializedBehaviorTree, "Changed Behavior Tree");

			if (edge != null)
			{
				((SerializedBehaviorTreeNode)edge.input.node).DisconnectParent();
				RemoveElement(edge);
			}

			SerializedProperty serializedBehaviorData = GetPropertyData(node);
			SerializedProperty children = serializedBehaviorData.FindPropertyRelative(ChildrenIndicesPropertyName);
			int childrenCount = children.arraySize;

			do
			{
				children.DeleteArrayElementAtIndex(index);
			} while (children.arraySize == childrenCount);

			m_serializedBehaviorTreeObject.ApplyModifiedProperties();

			Undo.CollapseUndoOperations(undoGroup);
		}

		public void CreateNewBehavior([NotNull] Type behaviorType, Vector2 position)
		{
			Undo.SetCurrentGroupName("Behavior Tree changed");
			int undoGroup = Undo.GetCurrentGroup();
			Undo.RegisterCompleteObjectUndo(m_serializedBehaviorTree, "Changed Behavior Tree");

			var behavior = (SerializedBehavior_Base)ScriptableObject.CreateInstance(behaviorType);
			behavior.name = behaviorType.Name;
			AssetDatabase.AddObjectToAsset(behavior, m_serializedBehaviorTree);
			var node = new SerializedBehaviorTreeNode(behavior, this);
			AddElement(node);
			m_nodes.Add(node);

			SerializedProperty serializedDatas = m_serializedBehaviorTreeObject
				.FindProperty(SerializedBehaviorDataPropertyName);
			int index = serializedDatas.arraySize++;
			SerializedProperty serializedData = serializedDatas.GetArrayElementAtIndex(index);
			serializedData.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue = behavior;
			position -= m_editorWindow.position.position;
			position = contentViewContainer.WorldToLocal(position);
			serializedData.FindPropertyRelative(NodeGraphInfoPropertyName)
					.FindPropertyRelative(PositionPropertyName).vector2Value = position;
			node.SetPosition(new Rect(position, s_defaultSize));

			node.RefreshExpandedState();
			node.RefreshPorts();

			m_serializedBehaviorTreeObject.ApplyModifiedProperties();

			Undo.CollapseUndoOperations(undoGroup);
		}

		public void Dispose()
		{
			Object.DestroyImmediate(m_searchWindowProvider);
			m_serializedBehaviorTree.OnAssetChanged -= Update;
		}

		private void Update()
		{
			m_serializedBehaviorTreeObject.Update();
			SerializedProperty serializedBehaviorDataArray =
				m_serializedBehaviorTreeObject.FindProperty(SerializedBehaviorDataPropertyName);

			UpdateDeletedBehaviors(serializedBehaviorDataArray);
			UpdateCreatedBehaviors(serializedBehaviorDataArray);
			UpdatePositions(serializedBehaviorDataArray);
			UpdateChildren(serializedBehaviorDataArray);
			UpdateRoot();
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
					((SerializedBehaviorTreeNode)edge.input.node).DisconnectParent();
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
					Edge removedEdge = removedEdges[edgeIndex];
					((SerializedBehaviorTreeNode)removedEdge.input.node).DisconnectParent();
					RemoveElement(removedEdge);
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
							((SerializedBehaviorTreeNode)edge.input.node).DisconnectParent();
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
						((SerializedBehaviorTreeNode)previousEdge.input.node).DisconnectParent();
						RemoveElement(previousEdge);
					}

					AddElement(node.SetChild(FindNode(child), childIndex));
				}
			}
		}

		private void UpdateRoot()
		{
			int rootNode = m_serializedBehaviorTreeObject.FindProperty(RootNodePropertyName).intValue;
			Vector2 position = m_serializedBehaviorTreeObject.FindProperty(RootGraphInfoPropertyName)
				.FindPropertyRelative(PositionPropertyName).vector2Value;

			if (rootNode < 0)
			{
				Edge edge = m_rootNode.Disconnect();

				if (edge != null)
				{
					((SerializedBehaviorTreeNode)edge.input.node).DisconnectParent();
					RemoveElement(edge);
				}
			}
			else
			{
				var rootBehavior = (SerializedBehavior_Base)m_serializedBehaviorTreeObject
					.FindProperty(SerializedBehaviorDataPropertyName)
					.GetArrayElementAtIndex(rootNode).FindPropertyRelative(SerializedBehaviorPropertyName)
					.objectReferenceValue;
				SerializedBehaviorTreeNode node = FindNode(rootBehavior);

				if (m_rootNode.GetChild() != node)
				{
					Edge edge = m_rootNode.Disconnect();

					if (edge != null)
					{
						((SerializedBehaviorTreeNode)edge.input.node).DisconnectParent();
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
			m_serializedBehaviorTreeObject.Update();

			Undo.SetCurrentGroupName("Behavior Tree changed");
			int undoGroup = Undo.GetCurrentGroup();
			Undo.RegisterCompleteObjectUndo(m_serializedBehaviorTree, "Changed Behavior Tree");

			if (graphViewChange.elementsToRemove != null)
			{
				RemoveElements(graphViewChange.elementsToRemove);
			}

			if (graphViewChange.edgesToCreate != null)
			{
				CreateEdges(graphViewChange.edgesToCreate);
			}

			if (graphViewChange.movedElements != null)
			{
				MoveElements(graphViewChange.movedElements, graphViewChange.moveDelta);
			}

			m_serializedBehaviorTreeObject.ApplyModifiedProperties();

			Undo.CollapseUndoOperations(undoGroup);

			return graphViewChange;
		}

		private void RemoveElements([NotNull] List<GraphElement> elementsToRemove)
		{
			elementsToRemove.Remove(m_rootNode);

			for (int i = 0, count = elementsToRemove.Count; i < count; ++i)
			{
				RemoveElement(elementsToRemove[i]);
			}

			RemoveEdges(elementsToRemove.OfType<Edge>());
			RemoveNodes(elementsToRemove.OfType<SerializedBehaviorTreeNode>());
		}

		private void RemoveEdges([NotNull] IEnumerable<Edge> edgesToRemove)
		{
			foreach (Edge edgeToRemove in edgesToRemove)
			{
				Node edgeNode = edgeToRemove.output.node;

				switch (edgeNode)
				{
					case SerializedBehaviorTreeNode treeNode:
						SerializedProperty serializedBehaviorData = GetPropertyData(treeNode);
						int edgeIndex = treeNode.IndexOfEdge(edgeToRemove);
						serializedBehaviorData.FindPropertyRelative(ChildrenIndicesPropertyName)
							.GetArrayElementAtIndex(edgeIndex).intValue = -1;
						treeNode.RemoveChild(edgeIndex);
						break;
					case RootNode _:
						m_serializedBehaviorTreeObject.FindProperty(RootNodePropertyName).intValue = -1;
						m_rootNode.Disconnect();
						break;
				}
			}
		}

		private void RemoveNodes([NotNull] IEnumerable<SerializedBehaviorTreeNode> nodes)
		{
			SerializedProperty datas = m_serializedBehaviorTreeObject.FindProperty(SerializedBehaviorDataPropertyName);

			foreach (SerializedBehaviorTreeNode node in nodes)
			{
				int nodeToRemoveIndex = GetPropertyDataIndex(node);

				if (nodeToRemoveIndex < 0)
				{
					continue;
				}

				for (int dataIndex = 0, dataCount = datas.arraySize; dataIndex < dataCount; ++dataIndex)
				{
					SerializedProperty children = datas.GetArrayElementAtIndex(dataIndex)
						.FindPropertyRelative(ChildrenIndicesPropertyName);

					for (int childIndex = 0, childrenCount = children.arraySize;
						childIndex < childrenCount;
						++childIndex)
					{
						SerializedProperty child = children.GetArrayElementAtIndex(childIndex);

						if (child.intValue > nodeToRemoveIndex)
						{
							--child.intValue;
						}
					}
				}

				Undo.DestroyObjectImmediate(datas.GetArrayElementAtIndex(nodeToRemoveIndex)
					.FindPropertyRelative(SerializedBehaviorPropertyName).objectReferenceValue);

				int datasSize = datas.arraySize;
				do
				{
					datas.DeleteArrayElementAtIndex(nodeToRemoveIndex);
				} while (datas.arraySize == datasSize);

				SerializedProperty rootNode = m_serializedBehaviorTreeObject.FindProperty(RootNodePropertyName);
				if (rootNode.intValue > nodeToRemoveIndex)
				{
					--rootNode.intValue;
				}

				m_nodes.Remove(node);
			}
		}

		private void CreateEdges([NotNull] List<Edge> edgesToCreate)
		{
			for (int i = 0, count = edgesToCreate.Count; i < count; ++i)
			{
				Edge edgeToCreate = edgesToCreate[i];
				var childNode = (SerializedBehaviorTreeNode)edgeToCreate.input.node;
				int childNodeIndex = GetPropertyDataIndex(childNode);
				Node parentNode = edgeToCreate.output.node;

				switch (parentNode)
				{
					case SerializedBehaviorTreeNode treeNode:
						int parentIndex = treeNode.GetPortIndex(edgeToCreate.output);
						SerializedProperty serializedBehaviorData = GetPropertyData(treeNode);
						serializedBehaviorData.FindPropertyRelative(ChildrenIndicesPropertyName)
							.GetArrayElementAtIndex(parentIndex).intValue = childNodeIndex;
						treeNode.SetChild(edgeToCreate, parentIndex);
						break;
					case RootNode _:
						m_serializedBehaviorTreeObject.FindProperty(RootNodePropertyName).intValue = childNodeIndex;
						m_rootNode.SetChild(edgeToCreate);
						break;
				}
			}
		}

		private void MoveElements(List<GraphElement> movedElements, Vector2 moveDelta)
		{
			for (int i = 0, count = movedElements.Count; i < count; ++i)
			{
				GraphElement movedElement = movedElements[i];

				switch (movedElement)
				{
					case SerializedBehaviorTreeNode treeNode:
						SerializedProperty serializedBehaviorData = GetPropertyData(treeNode);
						serializedBehaviorData.FindPropertyRelative(NodeGraphInfoPropertyName)
							.FindPropertyRelative(PositionPropertyName).vector2Value += moveDelta;
						break;
					case RootNode _:
						m_serializedBehaviorTreeObject.FindProperty(RootGraphInfoPropertyName)
							.FindPropertyRelative(PositionPropertyName).vector2Value += moveDelta;
						break;
				}
			}
		}

		private void OnCreateNode(NodeCreationContext context)
		{
			SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), m_searchWindowProvider);
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

		[CanBeNull]
		private SerializedProperty GetPropertyData([NotNull] SerializedBehaviorTreeNode node)
		{
			int index = GetPropertyDataIndex(node);
			return index < 0 ? null : m_serializedBehaviorTreeObject.FindProperty(SerializedBehaviorDataPropertyName)
				.GetArrayElementAtIndex(index);
		}

		private int GetPropertyDataIndex([NotNull] SerializedBehaviorTreeNode node)
		{
			SerializedProperty serializedData = m_serializedBehaviorTreeObject
				.FindProperty(SerializedBehaviorDataPropertyName);

			for (int i = 0, count = serializedData.arraySize; i < count; ++i)
			{
				SerializedProperty serializedBehaviorData = serializedData.GetArrayElementAtIndex(i);
				Object serializedBehavior = serializedBehaviorData.FindPropertyRelative(SerializedBehaviorPropertyName)
					.objectReferenceValue;

				if (serializedBehavior == node.dependedSerializedBehavior)
				{
					return i;
				}
			}

			return -1;
		}
	}
}
