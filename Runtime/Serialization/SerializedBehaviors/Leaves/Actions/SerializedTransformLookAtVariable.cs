// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Transform Property Name", 0), NameOverride("Target Property Name", 1),
	NameOverride("Up Property Name", 2)]
	[SearchGroup("Transform")]
	public sealed class SerializedTransformLookAtVariable :
		SerializedAction<TransformLookAtVariable, string, string, string>
	{
	}
}
