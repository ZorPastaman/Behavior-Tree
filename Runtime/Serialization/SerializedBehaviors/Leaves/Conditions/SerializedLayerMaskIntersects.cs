// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Value Property Name", 0), NameOverride("Mask", 1)]
	[SearchGroup("Layer Mask")]
	public sealed class SerializedLayerMaskIntersects : SerializedCondition<LayerMaskIntersects, string, LayerMask>
	{
	}
}
