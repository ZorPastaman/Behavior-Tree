// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Transform Property Name", 0), NameOverride("Target Property Name", 1), NameOverride("Up", 2)]
	[SearchGroup("Transform")]
	public sealed class SerializedTransformLookAt : SerializedAction<TransformLookAt, string, string, Vector3>
	{
	}
}
