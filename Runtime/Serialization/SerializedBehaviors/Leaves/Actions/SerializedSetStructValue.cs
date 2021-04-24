// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Value", 0), NameOverride("Property Name", 1)]
	public abstract class SerializedSetStructValue<T> : SerializedAction<SetStructValue<T>, T, string> where T : struct
	{
	}
}
