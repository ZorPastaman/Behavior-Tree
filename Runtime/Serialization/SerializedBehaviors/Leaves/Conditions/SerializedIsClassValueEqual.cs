// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Value", 0), NameOverride("Property Name", 1)]
	[SearchGroup("Comparison/Is Class Value Equal")]
	public abstract class SerializedIsClassValueEqual<T> : SerializedCondition<IsClassValueEqual<T>, T, string> where T : class
	{
	}
}
