// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Origin Property Name", 0), NameOverride("Direction Property Name", 1),
	NameOverride("Max Distance Property Name", 2), NameOverride("Layer Mask Property Name", 3),
	NameOverride("Hit Property Name", 4)]
	[SearchGroup("Physics")]
	public sealed class SerializedGetRaycastHitVariable :
		SerializedAction<GetRaycastHitVariable, string, string, string, string, string>
	{
	}
}
