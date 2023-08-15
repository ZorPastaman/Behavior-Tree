// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Color Property Name", 0), NameOverride("Alpha", 1)]
	[SearchGroup("Color")]
	public sealed class SerializedIsColorAlphaGreater : SerializedCondition<IsColorAlphaGreater, string, float>
	{
	}
}
