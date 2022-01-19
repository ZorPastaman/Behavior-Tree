﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Vector Property Name", 0), NameOverride("X", 1)]
	[SearchGroup("Vector2")]
	public sealed class SerializedIsVector2XLess : SerializedCondition<IsVector2XLess, string, float>
	{
	}
}
