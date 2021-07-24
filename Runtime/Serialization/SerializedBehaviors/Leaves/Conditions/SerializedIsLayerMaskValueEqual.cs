// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("First Operand Property Name", 0), NameOverride("Second Operand", 1)]
	[SearchGroup("Layer Mask")]
	public sealed class SerializedIsLayerMaskValueEqual : SerializedCondition<IsLayerMaskValueEqual, LayerMask, string>
	{
	}
}
