// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Color Property Name", 0), NameOverride("Green", 1)]
	[SearchGroup("Color")]
	public sealed class SerializedIsColorGreenLess : SerializedCondition<IsColorGreenLess, string, float>
	{
	}
}
