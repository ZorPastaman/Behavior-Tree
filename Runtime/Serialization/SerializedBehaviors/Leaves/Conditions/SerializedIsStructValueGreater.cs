// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Value", 0), NameOverride("Property Name", 1)]
	[SearchGroup("Comparison/Is Struct Value Greater")]
	public abstract class SerializedIsStructValueGreater<T> : SerializedCondition<IsStructValueGreater<T>, T, string>
		where T : struct, IComparable<T>
	{
	}
}
