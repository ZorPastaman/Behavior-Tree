// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Reference Property Name", 0), NameOverride("Compared Property Name", 1)]
	[SearchGroup("Comparison/Is Struct Value Greater Variable")]
	public abstract class SerializedIsStructValueGreaterVariable<T> :
		SerializedCondition<IsStructValueGreaterVariable<T>, string, string> where T : struct, IComparable<T>
	{
	}
}
