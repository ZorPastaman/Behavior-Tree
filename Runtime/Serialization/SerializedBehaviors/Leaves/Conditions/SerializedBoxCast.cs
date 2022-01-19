// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Center Property Name", 0), NameOverride("Half Extents Property Name", 1),
	NameOverride("Direction Property Name", 2), NameOverride("Orientation Property Name", 3),
	NameOverride("Max Distance", 4), NameOverride("Layer Mask", 5)]
	[SearchGroup("Physics")]
	public sealed class SerializedBoxCast :
		SerializedCondition<BoxCast, string, string, string, string, float, LayerMask>
	{
	}
}
