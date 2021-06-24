// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Collider Property Name", 0), NameOverride("Origin Property Name", 1),
	NameOverride("Direction Property Name", 2)]
	[SearchGroup("Collider")]
	public sealed class SerializedIsColliderRaycast : SerializedCondition<IsColliderRaycast, string, string, string>
	{
	}
}
