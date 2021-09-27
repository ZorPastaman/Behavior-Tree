// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Transform Property Name", 0), NameOverride("Translation", 1)]
	[SearchGroup("Transform")]
	public sealed class SerializedTransformTranslate : SerializedAction<TransformTranslate, string, Vector3>
	{
	}
}
