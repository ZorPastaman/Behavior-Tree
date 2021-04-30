// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Property Name", 0)]
	[SearchGroup("Remove Struct Value")]
	public abstract class SerializedRemoveStructValue<T> : SerializedAction<RemoveStructValue<T>, string>
		where T : struct
	{
	}
}
