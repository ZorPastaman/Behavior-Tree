﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Game Object Property Name", 0), NameOverride("Component Property Name", 1)]
	[SearchGroup("Game Object/Add Component")]
	public abstract class SerializedAddComponent<T> : SerializedAction<AddComponent<T>, string, string>
		where T : Component
	{
	}
}
