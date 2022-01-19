// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("First Property Name", 0), NameOverride("Second Property Name", 1)]
	[SearchGroup("Comparison/Is Class Value Equal Variable")]
	public abstract class SerializedIsClassValueEqualVariable<T> :
		SerializedCondition<IsClassValueEqualVariable<T>, string, string> where T : class
	{
	}
}
