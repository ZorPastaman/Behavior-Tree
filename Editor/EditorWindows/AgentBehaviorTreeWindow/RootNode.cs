// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow
{
	public sealed class RootNode : Node
	{
		public RootNode()
		{
			title = "Root";

			Port output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(SerializedBehaviorTreeWindow.BehaviorConnection));
			output.portName = "First";
			outputContainer.Add(output);

			var color = new Color(1f, 0.65f, 0f, 1f);
			IStyle mainStyle = mainContainer.style;
			mainStyle.borderTopColor = mainStyle.borderBottomColor =
				mainStyle.borderLeftColor = mainStyle.borderRightColor = color;
			color.a = elementTypeColor.a;
			elementTypeColor = color;
		}

		public Edge AddChild([NotNull] AgentBehaviorTreeNode node)
		{
			return ((Port)outputContainer[0]).ConnectTo((Port)node.inputContainer[0]);
		}
	}
}
