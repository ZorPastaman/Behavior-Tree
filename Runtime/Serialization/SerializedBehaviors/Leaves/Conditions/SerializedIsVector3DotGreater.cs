// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("From Property Name", 0), NameOverride("To Property Name", 1), NameOverride("Dot", 2)]
	[SearchGroup("Vector3")]
	public sealed class SerializedIsVector3DotGreater :
		SerializedCondition<IsVector3DotGreater, string, string, float>
	{
	}
}
