﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("First Property Name", 0), NameOverride("Second Property Name", 1)]
	[SearchGroup("Comparison/Is Struct Value Less Variable")]
	public abstract class SerializedIsStructValueLessVariable<T> :
		SerializedCondition<IsStructValueLessVariable<T>, string, string> where T : struct, IComparable<T>
	{
	}
}
