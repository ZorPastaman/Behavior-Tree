// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Value Property Name", 0), NameOverride("Layer Mask Property Name", 1)]
	[SearchGroup("Layer Mask")]
	public sealed class SerializedValueToLayerMask : SerializedAction<ValueToLayerMask, string, string>
	{
	}
}
