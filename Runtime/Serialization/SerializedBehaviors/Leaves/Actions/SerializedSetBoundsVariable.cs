// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Center Property Name", 0), NameOverride("Size Property Name", 1),
	NameOverride("Bounds Property Name", 2)]
	[SearchGroup("Bounds")]
	public sealed class SerializedSetBoundsVariable : SerializedAction<SetBoundsVariable, string, string, string>
	{
	}
}
