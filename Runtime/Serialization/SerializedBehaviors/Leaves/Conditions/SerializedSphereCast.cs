// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Origin Property Name", 0), NameOverride("Radius Property Name", 1),
	NameOverride("Direction Property Name", 2), NameOverride("Max Distance", 3), NameOverride("Layer Mask", 4)]
	[SearchGroup("Physics")]
	public sealed class SerializedSphereCast : SerializedCondition<SphereCast, string, string, string, float, LayerMask>
	{
	}
}
