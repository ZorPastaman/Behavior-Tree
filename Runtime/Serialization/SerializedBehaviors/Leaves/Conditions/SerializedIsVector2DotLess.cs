// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("From Property Name", 0), NameOverride("To Property Name", 1), NameOverride("Dot", 2)]
	[SearchGroup("Vector2")]
	public sealed class SerializedIsVector2DotLess : SerializedCondition<IsVector2DotLess, string, string, float>
	{
	}
}
