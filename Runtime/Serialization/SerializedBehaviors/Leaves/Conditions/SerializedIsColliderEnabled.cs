// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Collider Property Name", 0)]
	[SearchGroup("Collider")]
	public sealed class SerializedIsColliderEnabled : SerializedCondition<IsColliderEnabled, string>
	{
	}
}
