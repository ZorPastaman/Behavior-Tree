﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("First Operand Property Name", 0), NameOverride("Second Operand Property Name", 1),
	NameOverride("Result", 2)]
	[SearchGroup("Layer Mask")]
	public sealed class SerializedLayerMaskAndVariable : SerializedAction<LayerMaskAndVariable, string, string, string>
	{
	}
}
