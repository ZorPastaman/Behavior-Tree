// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.StatusBehaviors
{
	[NameOverride("Property Name", 0)]
	public sealed class SerializedVariableBehavior : SerializedStatusBehavior<VariableBehavior, string>
	{
	}
}
