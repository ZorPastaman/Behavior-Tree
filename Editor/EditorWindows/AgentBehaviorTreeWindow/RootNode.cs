// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow
{
	/// <summary>
	/// UI root node. It's not a view of a <see cref="Behavior"/>. It's used to easily select a root node in ui.
	/// </summary>
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
