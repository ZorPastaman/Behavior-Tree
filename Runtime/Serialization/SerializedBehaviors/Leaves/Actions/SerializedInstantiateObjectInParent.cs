// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Prefab Property Name", 0), NameOverride("Parent Property Name", 1),
	NameOverride("Result Property Name", 2)]
	[SearchGroup("Unity Object")]
	public sealed class SerializedInstantiateObjectInParent :
		SerializedAction<InstantiateObjectInParent, string, string, string>
	{
	}
}
