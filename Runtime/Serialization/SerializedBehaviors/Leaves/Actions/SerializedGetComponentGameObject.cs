﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Component Property Name", 0), NameOverride("Game Object Property Name", 1)]
	[SearchGroup("Component")]
	public sealed class SerializedGetComponentGameObject : SerializedAction<GetComponentGameObject, string, string>
	{
	}
}
