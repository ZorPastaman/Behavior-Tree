// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Vector Property Name", 0), NameOverride("Y Property Name", 1)]
	[SearchGroup("Vector2")]
	public sealed class SerializedIsVector2YLessVariable : SerializedCondition<IsVector2YLessVariable, string, string>
	{
	}
}
