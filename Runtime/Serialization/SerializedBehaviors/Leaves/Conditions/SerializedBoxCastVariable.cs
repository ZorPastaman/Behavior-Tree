// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Center Property Name", 0), NameOverride("Half Extents Property Name", 1),
	NameOverride("Direction Property Name", 2), NameOverride("Orientation Property Name", 3),
	NameOverride("Max Distance Property Name", 4), NameOverride("Layer Mask Property Name", 5)]
	[SearchGroup("Physics")]
	public sealed class SerializedBoxCastVariable :
		SerializedCondition<BoxCastVariable, string, string, string, string, string, string>
	{
	}
}
