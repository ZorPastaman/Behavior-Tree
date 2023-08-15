// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Agent Property Name", 0), NameOverride("Area Mask Property Name", 1),
	NameOverride("Max Distance Property Name", 2), NameOverride("Hit Property Name", 3)]
	[SearchGroup("Nav Mesh Agent")]
	public sealed class SerializedNavMeshAgentSamplePathPositionVariable :
		SerializedAction<NavMeshAgentSamplePathPositionVariable, string, string, string, string>
	{
	}
}
