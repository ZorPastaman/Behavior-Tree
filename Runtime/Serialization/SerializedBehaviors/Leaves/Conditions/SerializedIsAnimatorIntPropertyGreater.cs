// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Animator Property Name", 0), NameOverride("Property Name", 1), NameOverride("Value", 2)]
	[SearchGroup("Animator/Property Comparison")]
	public sealed class SerializedIsAnimatorIntPropertyGreater :
		SerializedCondition<IsAnimatorIntPropertyGreater, string, string, int>
	{
	}
}
