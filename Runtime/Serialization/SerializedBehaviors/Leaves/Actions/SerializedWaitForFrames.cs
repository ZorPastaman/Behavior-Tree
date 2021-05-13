// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Duration", 0)]
	[SearchGroup("Waits")]
	public sealed class SerializedWaitForFrames : SerializedAction<WaitForFrames, int>
	{
	}
}
