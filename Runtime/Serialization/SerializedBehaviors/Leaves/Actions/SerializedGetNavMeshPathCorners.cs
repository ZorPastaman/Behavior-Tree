// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Path Property Name", 0), NameOverride("Corners Property Name", 1)]
	[SearchGroup("Nav Mesh Path")]
	public sealed class SerializedGetNavMeshPathCorners : SerializedAction<GetNavMeshPathCorners, string, string>
	{
	}
}
