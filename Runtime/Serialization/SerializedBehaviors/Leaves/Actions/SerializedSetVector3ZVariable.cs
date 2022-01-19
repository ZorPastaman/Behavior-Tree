﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("Z Property Name", 1),
	NameOverride("Result Property Name", 2)]
	[SearchGroup("Vector3")]
	public sealed class SerializedSetVector3ZVariable : SerializedAction<SetVector3ZVariable, string, string, string>
	{
	}
}
