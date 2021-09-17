﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Quaternion Property Name", 0), NameOverride("X", 1)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedIsQuaternionXLess : SerializedCondition<IsQuaternionXLess, string, float>
	{
	}
}