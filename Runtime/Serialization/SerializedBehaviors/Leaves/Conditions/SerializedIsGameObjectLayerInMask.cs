// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Game Object Property Name", 0), NameOverride("Layer Mask", 1)]
	[SearchGroup("Game Object")]
	public sealed class SerializedIsGameObjectLayerInMask :
		SerializedCondition<IsGameObjectLayerInMask, string, LayerMask>
	{
	}
}
