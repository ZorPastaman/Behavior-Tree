// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Collider Property Name", 0), NameOverride("Origin Property Name", 1),
	NameOverride("Direction Property Name", 2), NameOverride("Hit Property Name", 3)]
	[SearchGroup("Collider")]
	public sealed class SerializedRaycastCollider : SerializedAction<RaycastCollider, string, string, string, string>
	{
	}
}
