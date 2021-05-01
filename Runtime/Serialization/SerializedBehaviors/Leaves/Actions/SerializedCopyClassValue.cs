// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Source Property Name", 0), NameOverride("Destination Property Name", 1)]
	[SearchGroup("Copy Class Value")]
	public abstract class SerializedCopyClassValue<T> : SerializedAction<CopyClassValue<T>, string, string>
		where T : class
	{
	}
}
