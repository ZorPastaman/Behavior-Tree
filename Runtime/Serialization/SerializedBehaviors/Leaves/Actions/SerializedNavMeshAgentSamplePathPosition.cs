// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Agent Property Name", 0), NameOverride("Max Distance", 1), NameOverride("Hit Property Name", 2)]
	[SearchGroup("Nav Mesh Agent")]
	public sealed class SerializedNavMeshAgentSamplePathPosition :
		SerializedAction<NavMeshAgentSamplePathPosition, string, float, string>
	{
	}
}
