// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Point 1 Property Name", 0), NameOverride("Point 2 Property Name", 1),
	NameOverride("Radius Property Name", 2), NameOverride("Direction Property Name", 3),
	NameOverride("Max Distance", 4), NameOverride("Layer Mask", 5), NameOverride("Hit Property Name", 6)]
	[SearchGroup("Physics")]
	public sealed class SerializedGetCapsuleCastHit :
		SerializedAction<GetCapsuleCastHit, string, string, string, string, float, LayerMask, string>
	{
	}
}
