// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Collider Property Name", 0), NameOverride("Rigidbody Property Name", 1)]
	[SearchGroup("Collider")]
	public sealed class SerializedGetColliderRigidbody : SerializedAction<GetColliderRigidbody, string, string>
	{
	}
}
