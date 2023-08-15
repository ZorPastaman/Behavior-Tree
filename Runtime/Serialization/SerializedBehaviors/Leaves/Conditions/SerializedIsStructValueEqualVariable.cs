// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("First Property Name", 0), NameOverride("Second Property Name", 1)]
	[SearchGroup("Comparison/Is Struct Value Equal Variable")]
	public abstract class SerializedIsStructValueEqualVariable<T> :
		SerializedCondition<IsStructValueEqualVariable<T>, string, string> where T : struct, IEquatable<T>
	{
	}
}
