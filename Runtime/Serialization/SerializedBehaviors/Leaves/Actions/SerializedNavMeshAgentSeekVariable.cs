// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Agent Property Name", 0), NameOverride("Sought Property Name", 1),
	NameOverride("Recalculate Tolerance Property Name", 2), NameOverride("Reach Distance Property Name", 3),
	NameOverride("Holder Property Name", 3)]
	[SearchGroup("Nav Mesh Agent")]
	public sealed class SerializedNavMeshAgentSeekVariable :
		SerializedAction<NavMeshAgentSeekVariable, string, string, string, string, string>
	{
	}
}
