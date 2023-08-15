// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Center Property Name", 0), NameOverride("Half Extents Property Name", 1),
	NameOverride("Orientation Property Name", 2), NameOverride("Layer Mask Property Name", 3)]
	[SearchGroup("Physics")]
	public sealed class SerializedCheckBoxVariable :
		SerializedCondition<CheckBoxVariable, string, string, string, string>
	{
	}
}
