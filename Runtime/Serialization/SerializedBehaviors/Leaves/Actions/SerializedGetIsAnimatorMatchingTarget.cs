﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Animator Property Name", 0), NameOverride("Value Property Name", 1)]
	[SearchGroup("Animator")]
	public sealed class SerializedGetIsAnimatorMatchingTarget :
		SerializedAction<GetIsAnimatorMatchingTarget, string, string>
	{
	}
}
