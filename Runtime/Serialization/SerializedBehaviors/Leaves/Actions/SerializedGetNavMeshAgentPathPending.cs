// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Agent Property Name", 0), NameOverride("Path Pending Property Name", 1)]
	[SearchGroup("Nav Mesh Agent")]
	public sealed class SerializedGetNavMeshAgentPathPending :
		SerializedAction<GetNavMeshAgentPathPending, string, string>
	{
	}
}
