// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("First Operand Property Name", 0), NameOverride("Second Operand", 1), NameOverride("Result", 2)]
	[SearchGroup("Layer Mask")]
	public sealed class SerializedLayerMaskAnd : SerializedAction<LayerMaskAnd, string, LayerMask, string>
	{
	}
}
