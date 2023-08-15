﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Origin Property Name", 0), NameOverride("Direction Property Name", 1),
	NameOverride("Max Distance", 2), NameOverride("Layer Mask", 3)]
	[SearchGroup("Physics")]
	public sealed class SerializedRaycast : SerializedCondition<Raycast, string, string, float, LayerMask>
	{
	}
}
