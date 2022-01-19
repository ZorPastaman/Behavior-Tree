// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Rigidbody Property Name", 0), NameOverride("Velocity", 1)]
	[SearchGroup("Rigidbody")]
	public sealed class SerializedIsRigidbodyVelocityGreater :
		SerializedCondition<IsRigidbodyVelocityGreater, string, float>
	{
	}
}
