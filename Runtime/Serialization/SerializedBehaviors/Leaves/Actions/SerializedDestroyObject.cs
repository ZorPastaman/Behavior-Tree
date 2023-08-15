// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Object Property Name", 0)]
	[SearchGroup("Unity Object")]
	public sealed class SerializedDestroyObject : SerializedAction<DestroyObject, string>
	{
	}
}
