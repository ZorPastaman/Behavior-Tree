// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Origin Property Name", 0), NameOverride("Radius Property Name", 1),
	NameOverride("Direction Property Name", 2), NameOverride("Max Distance", 3), NameOverride("Layer Mask", 5),
	NameOverride("Hit Property Name", 6)]
	[SearchGroup("Physics")]
	public sealed class SerializedGetSphereCastHit :
		SerializedAction<GetSphereCastHit, string, string, string, float, LayerMask, string>
	{
	}
}
