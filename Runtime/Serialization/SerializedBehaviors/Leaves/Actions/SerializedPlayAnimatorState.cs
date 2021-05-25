// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Animator Property Name", 0), NameOverride("State Name", 1), NameOverride("Layer", 2)]
	[SearchGroup("Animator")]
	public sealed class SerializedPlayAnimatorState : SerializedAction<PlayAnimatorState, string, string, int>
	{
	}
}
