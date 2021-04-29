// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Helpers;

namespace Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow
{
	public sealed class AgentBehaviorTreeNode : Node
	{
		[NotNull] private Behavior m_behavior;

		public AgentBehaviorTreeNode([NotNull] Behavior behavior)
		{
			m_behavior = behavior;

			title = TypeHelper.GetUIName(behavior.GetType());

			Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
				typeof(BehaviorConnection));
			input.portName = "Parent";
			inputContainer.Add(input);
		}

		public Edge AddChild([NotNull] AgentBehaviorTreeNode child)
		{
			Port output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(SerializedBehaviorTreeWindow.BehaviorConnection));
			output.portName = "Child";
			outputContainer.Add(output);

			return output.ConnectTo((Port)child.inputContainer[0]);
		}

		public void UpdateStatus()
		{
			Color statusColor;

			switch (m_behavior.status)
			{
				case Status.Idle:
					statusColor = Color.gray;
					break;
				case Status.Success:
					statusColor = Color.green;
					break;
				case Status.Running:
					statusColor = Color.yellow;
					break;
				case Status.Failure:
					statusColor = Color.red;
					break;
				case Status.Error:
					statusColor = Color.cyan;
					break;
				case Status.Abort:
					statusColor = Color.white;
					break;
				default:
					statusColor = Color.magenta;
					break;
			}

			IStyle mainStyle = mainContainer.style;
			mainStyle.borderTopColor = mainStyle.borderBottomColor =
				mainStyle.borderLeftColor = mainStyle.borderRightColor = statusColor;
			statusColor.a = elementTypeColor.a;
			elementTypeColor = statusColor;
		}
	}
}
