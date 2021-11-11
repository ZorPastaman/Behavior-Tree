// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Hit Property Name", 0), NameOverride("Distance Property Name", 1)]
	[SearchGroup("Nav Mesh Hit")]
	public sealed class SerializedIsNavMeshHitDistanceGreaterVariable :
		SerializedCondition<IsNavMeshHitDistanceGreaterVariable, string, string>
	{
	}
}
