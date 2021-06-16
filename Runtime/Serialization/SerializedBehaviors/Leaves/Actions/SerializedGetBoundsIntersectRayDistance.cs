// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Bounds Property Name", 0), NameOverride("Ray Property Name", 1),
	NameOverride("Distance Property Name", 2)]
	[SearchGroup("Bounds")]
	public sealed class SerializedGetBoundsIntersectRayDistance :
		SerializedAction<GetBoundsIntersectRayDistance, string, string, string>
	{
	}
}
