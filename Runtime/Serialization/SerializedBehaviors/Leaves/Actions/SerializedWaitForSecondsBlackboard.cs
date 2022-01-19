// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Time Property Name", 0), NameOverride("Duration", 1)]
	[SearchGroup("Waits")]
	public sealed class SerializedWaitForSecondsBlackboard : SerializedAction<WaitForSecondsBlackboard, string, float>
	{
	}
}
