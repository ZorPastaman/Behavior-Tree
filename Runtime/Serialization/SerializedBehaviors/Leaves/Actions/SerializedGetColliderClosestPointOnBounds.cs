// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Collider Property Name", 0), NameOverride("Point Property Name", 1),
	NameOverride("Closest Point Property Name", 2)]
	[SearchGroup("Collider")]
	public sealed class SerializedGetColliderClosestPointOnBounds :
		SerializedAction<GetColliderClosestPointOnBounds, string, string, string>
	{
	}
}
