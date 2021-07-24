// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Value Property Name", 0), NameOverride("Complement Property Name", 1)]
	[SearchGroup("Layer Mask")]
	public sealed class SerializedLayerMaskComplement : SerializedAction<LayerMaskComplement, string, string>
	{
	}
}
