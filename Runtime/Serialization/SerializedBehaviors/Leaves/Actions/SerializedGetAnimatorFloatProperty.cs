// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Animator Property Name", 0), NameOverride("Property Name", 1),
	NameOverride("Value Property Name", 2)]
	[SearchGroup("Animator/Get Property")]
	public sealed class SerializedGetAnimatorFloatProperty :
		SerializedAction<GetAnimatorFloatProperty, string, string, string>
	{
	}
}
