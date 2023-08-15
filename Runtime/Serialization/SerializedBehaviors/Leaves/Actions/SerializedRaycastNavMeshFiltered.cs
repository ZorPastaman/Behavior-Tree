// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Source Property Name", 0), NameOverride("Target Property Name", 1),
	NameOverride("Filter Property Name", 2), NameOverride("Hit Property Name", 3)]
	[SearchGroup("Nav Mesh")]
	public sealed class SerializedRaycastNavMeshFiltered :
		SerializedAction<RaycastNavMeshFiltered, string, string, string, string>
	{
	}
}
