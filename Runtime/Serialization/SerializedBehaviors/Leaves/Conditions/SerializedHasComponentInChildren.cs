// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Game Object Property Name", 0)]
	[SearchGroup("Game Object/Has Component In Children")]
	public abstract class SerializedHasComponentInChildren<T> : SerializedCondition<HasComponentInChildren<T>, string>
		where T : Component
	{
	}
}
