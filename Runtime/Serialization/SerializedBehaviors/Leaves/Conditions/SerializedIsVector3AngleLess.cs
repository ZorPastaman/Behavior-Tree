// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("From Property Name", 0), NameOverride("To Property Name", 1), NameOverride("Angle", 2)]
	[SearchGroup("Vector3")]
	public sealed class SerializedIsVector3AngleLess : SerializedCondition<IsVector3AngleLess, string, string, float>
	{
	}
}
