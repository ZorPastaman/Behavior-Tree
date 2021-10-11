﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("Magnitude Property Name", 1)]
	[SearchGroup("Vector3")]
	public sealed class SerializedGetVector3Magnitude : SerializedAction<GetVector3Magnitude, string, string>
	{
	}
}