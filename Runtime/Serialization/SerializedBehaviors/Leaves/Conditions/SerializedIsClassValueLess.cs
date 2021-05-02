// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Value", 0), NameOverride("Property Name", 1)]
	[SearchGroup("Comparison/Is Class Value Less")]
	public abstract class SerializedIsClassValueLess<T> : SerializedCondition<IsClassValueLess<T>, T, string>
		where T : class, IComparable<T>
	{
	}
}
