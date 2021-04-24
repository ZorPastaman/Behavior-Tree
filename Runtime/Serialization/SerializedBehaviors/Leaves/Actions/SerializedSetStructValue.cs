// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	public abstract class SerializedSetStructValue<T> : SerializedAction<SetStructValue<T>, T, string> where T : struct
	{
	}
}
