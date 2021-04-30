// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Property Name", 0)]
	[SearchGroup("Has Class Value")]
	public abstract class SerializedHasClassValue<T> : SerializedCondition<HasClassValue<T>, string> where T : class
	{
	}
}
