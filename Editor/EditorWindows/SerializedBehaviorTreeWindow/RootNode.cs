// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEditor.Experimental.GraphView;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class RootNode : Node
	{
		public RootNode()
		{
			title = "Root";

			Port output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(BehaviorConnection));
			output.portName = "First";
			outputContainer.Add(output);
		}
	}
}
