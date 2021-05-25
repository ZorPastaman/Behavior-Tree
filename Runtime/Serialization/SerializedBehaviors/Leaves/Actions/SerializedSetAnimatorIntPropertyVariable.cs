﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Animator Property Name", 0), NameOverride("Property Name", 1),
	NameOverride("Value Property Name", 2)]
	[SearchGroup("Animator/Set Property")]
	public sealed class SerializedSetAnimatorIntPropertyVariable :
		SerializedAction<SetAnimatorIntPropertyVariable, string, string, string>
	{
	}
}
