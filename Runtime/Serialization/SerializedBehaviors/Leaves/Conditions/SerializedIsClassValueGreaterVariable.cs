// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Left Property Name", 0), NameOverride("Right Property Name", 1)]
	[SearchGroup("Comparison/Is Class Value Greater Variable")]
	public abstract class SerializedIsClassValueGreaterVariable<T> :
		SerializedCondition<IsClassValueGreaterVariable<T>, string, string> where T : class, IComparable<T>
	{
	}
}
