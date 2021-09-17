﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Quaternion Property Name", 0), NameOverride("Z Property Name", 1)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedIsQuaternionZLessVariable :
		SerializedCondition<IsQuaternionZLessVariable, string, string>
	{
	}
}