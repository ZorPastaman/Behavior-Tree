// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Object Property Name", 0), NameOverride("Parent Property Name", 1),
	NameOverride("Position Property Name", 2), NameOverride("Rotation Property Name", 3),
	NameOverride("Result Property Name", 4)]
	[SearchGroup("Unity Object")]
	public sealed class SerializedInstantiateObjectInParentAndPose :
		SerializedAction<InstantiateObjectInParentAndPose, string, string, string, string, string>
	{
	}
}
