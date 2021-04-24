// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class RootNode : Node
	{
		private Edge m_outputEdge;

		public RootNode()
		{
			title = "Root";

			Port output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(BehaviorConnection));
			output.portName = "First";
			outputContainer.Add(output);
		}

		[CanBeNull]
		public Edge outputEdge => m_outputEdge;

		[CanBeNull]
		public SerializedBehaviorTreeNode GetChild()
		{
			return m_outputEdge?.input.node as SerializedBehaviorTreeNode;
		}

		[NotNull]
		public Edge Connect(SerializedBehaviorTreeNode child)
		{
			m_outputEdge = ((Port)outputContainer[0]).ConnectTo((Port)child.inputContainer[0]);
			return m_outputEdge;
		}

		public void SetChild([NotNull] Edge edge)
		{
			m_outputEdge = edge;
		}

		[CanBeNull]
		public Edge Disconnect()
		{
			if (m_outputEdge == null)
			{
				return null;
			}

			Edge answer = m_outputEdge;
			((Port)outputContainer[0]).Disconnect(m_outputEdge);
			m_outputEdge = null;

			return answer;
		}
	}
}
