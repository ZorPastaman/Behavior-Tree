// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Agent Property Name", 0), NameOverride("Avoided Property Name", 1),
	NameOverride("Min Distance To Avoided Property Name", 2), NameOverride("Min Dot To Avoided Property Name", 3),
	NameOverride("Avoid Distance Property Name", 4), NameOverride("Holder Property Name", 5)]
	[SearchGroup("Nav Mesh Agent")]
	public sealed class SerializedNavMeshAgentAvoidVariable :
		SerializedAction<NavMeshAgentAvoidVariable, string, string, string, string, string, string>
	{
	}
}
