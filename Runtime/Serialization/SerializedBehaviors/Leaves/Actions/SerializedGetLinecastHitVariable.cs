// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Origin Property Name", 0), NameOverride("End Property Name", 1),
	NameOverride("LayerMask", 2), NameOverride("Hit Property Name", 3)]
	[SearchGroup("Physics")]
	public sealed class SerializedGetLinecastHitVariable :
		SerializedAction<GetLinecastHitVariable, string, string, string, string>
	{
	}
}
