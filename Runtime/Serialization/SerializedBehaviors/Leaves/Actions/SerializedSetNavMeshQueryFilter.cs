// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Agent Type Id Property Name", 0), NameOverride("Area Mask Property Name", 1),
	NameOverride("Filter Property Name", 2)]
	[SearchGroup("Nav Mesh Query Filter")]
	public sealed class SerializedSetNavMeshQueryFilter :
		SerializedAction<SetNavMeshQueryFilter, string, string, string>
	{
	}
}
