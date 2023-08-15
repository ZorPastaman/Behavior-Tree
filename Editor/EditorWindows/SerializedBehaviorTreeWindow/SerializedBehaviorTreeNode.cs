// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Helpers;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	/// <summary>
	/// Serialized behavior tree node view.
	/// </summary>
	public sealed class SerializedBehaviorTreeNode : Node
	{
		[NotNull] private readonly SerializedBehavior_Base m_dependedSerializedBehavior;
		[NotNull] private readonly SerializedBehaviorTreeGraph m_treeGraph;
		private readonly bool m_isComposite;

		[NotNull] private readonly List<Edge> m_outputEdges = new List<Edge>();

		public SerializedBehaviorTreeNode([NotNull] SerializedBehavior_Base dependedSerializedBehavior,
			[NotNull] SerializedBehaviorTreeGraph treeGraph)
		{
			m_dependedSerializedBehavior = dependedSerializedBehavior;
			m_treeGraph = treeGraph;

			Type type = m_dependedSerializedBehavior.serializedBehaviorType;
			title = TypeHelper.GetUIName(type);
			m_isComposite = type.IsSubclassOf(typeof(Composite));

			Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
				typeof(BehaviorConnection));
			input.portName = "Parent";
			inputContainer.Add(input);

			if (m_isComposite)
			{
				var addButton = new Button(AddOutputPortAndNotify) {text = "+"};
				titleButtonContainer.Add(addButton);
			}

			UIElementsHelper.CreatePropertyElements(new SerializedObject(m_dependedSerializedBehavior),
				extensionContainer);
		}

		[NotNull]
		public SerializedBehavior_Base dependedSerializedBehavior => m_dependedSerializedBehavior;

		public int outputEdgeCount => m_outputEdges.Count;

		public void SetOutputCapacity([NotNull] List<Edge> removedEdges, int capacity)
		{
			if (m_outputEdges.Count > capacity)
			{
				for (int i = 0, count = m_outputEdges.Count - capacity; i < count; ++i)
				{
					int index = m_outputEdges.Count - 1;
					Edge edgeToRemove = RemoveOutputPort(index);

					if (edgeToRemove != null)
					{
						removedEdges.Add(edgeToRemove);
					}
				}
			}
			else if (m_outputEdges.Count < capacity)
			{
				for (int i = 0, count = capacity - m_outputEdges.Count; i < count; ++i)
				{
					AddOutputPort();
				}
			}
		}

		[CanBeNull]
		public SerializedBehaviorTreeNode GetChild(int index)
		{
			Edge edge = m_outputEdges[index];
			return edge?.input.node as SerializedBehaviorTreeNode;
		}

		[NotNull]
		public Edge SetChild([NotNull] SerializedBehaviorTreeNode child, int index)
		{
			var outputPort = (Port)outputContainer[index];
			var inputPort = (Port)child.inputContainer[0];
			Edge newEdge = outputPort.ConnectTo(inputPort);
			m_outputEdges[index] = newEdge;

			return newEdge;
		}

		public void SetChild([NotNull] Edge edge, int index)
		{
			m_outputEdges[index] = edge;
		}

		[CanBeNull]
		public Edge RemoveChild(int index)
		{
			Edge edge = m_outputEdges[index];

			if (edge != null)
			{
				((Port)outputContainer[index]).Disconnect(edge);
			}

			m_outputEdges[index] = null;

			return edge;
		}

		public int IndexOfEdge([NotNull] Edge edge)
		{
			return m_outputEdges.IndexOf(edge);
		}

		public int GetPortIndex([NotNull] Port port)
		{
			return outputContainer.IndexOf(port);
		}

		public void DisconnectParent()
		{
			((Port)inputContainer[0]).DisconnectAll();
		}

		private void AddOutputPort()
		{
			Port childPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(BehaviorConnection));
			childPort.portName = "Child";
			outputContainer.Add(childPort);

			if (m_isComposite)
			{
				var removeButton = new Button(() =>
				{
					if (m_outputEdges.Count > 2)
					{
						RemoveOutputPortAndNotify(childPort);
					}
				}) {text = "Del"};
				childPort.Add(removeButton);
			}

			m_outputEdges.Add(null);
		}

		[CanBeNull]
		private Edge RemoveOutputPort(int index)
		{
			Edge removedEdge = m_outputEdges[index];
			outputContainer.RemoveAt(index);
			m_outputEdges.RemoveAt(index);

			return removedEdge;
		}

		private void AddOutputPortAndNotify()
		{
			AddOutputPort();
			m_treeGraph.OnPortAdded(this);
		}

		private void RemoveOutputPortAndNotify([NotNull] Port port)
		{
			int index = outputContainer.IndexOf(port);
			Edge edge = RemoveOutputPort(index);
			m_treeGraph.OnPortRemoved(this, edge, index);
		}
	}
}
