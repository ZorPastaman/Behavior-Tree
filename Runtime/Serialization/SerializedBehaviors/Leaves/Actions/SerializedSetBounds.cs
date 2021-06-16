// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Center", 0), NameOverride("Size", 1), NameOverride("Bounds Property Name", 2)]
	[SearchGroup("Bounds")]
	public sealed class SerializedSetBounds : SerializedAction<SetBounds, Vector3, Vector3, string>
	{
	}
}
