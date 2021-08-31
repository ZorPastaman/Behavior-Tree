// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Center Property Name", 0), NameOverride("Half Extents Property Name", 1),
	NameOverride("Orientation Property Name", 2), NameOverride("Layer Mask", 3)]
	[SearchGroup("Physics")]
	public sealed class SerializedCheckBox : SerializedCondition<CheckBox, string, string, string, LayerMask>
	{
	}
}
