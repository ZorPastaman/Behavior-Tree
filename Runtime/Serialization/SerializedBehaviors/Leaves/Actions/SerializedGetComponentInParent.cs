// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	public abstract class SerializedGetComponentInParent<T> : SerializedAction<GetComponentInParent<T>, string, string>
		where T : Component
	{
	}
}
