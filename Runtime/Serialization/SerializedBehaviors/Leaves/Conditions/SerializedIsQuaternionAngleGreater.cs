﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("First Operand Property Name", 0), NameOverride("Second Operand Property Name", 1),
	NameOverride("Angle", 2)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedIsQuaternionAngleGreater :
		SerializedCondition<IsQuaternionAngleGreater, string, string, float>
	{
	}
}
