// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Origin Property Name", 0), NameOverride("Radius Property Name", 1),
	NameOverride("Direction Property Name", 2), NameOverride("Max Distance Property Name", 3),
	NameOverride("Layer Mask Property Name", 5), NameOverride("Hit Property Name", 6)]
	[SearchGroup("Physics")]
	public sealed class SerializedGetSphereCastHitVariable :
		SerializedAction<GetSphereCastHitVariable, string, string, string, string, string, string>
	{
	}
}
