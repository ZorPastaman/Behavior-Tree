﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Source Property Name", 0), NameOverride("Target Property Name", 1),
	NameOverride("Path Property Name", 2)]
	[SearchGroup("Nav Mesh")]
	public sealed class SerializedCalculateNavMeshPath : SerializedAction<CalculateNavMeshPath, string, string, string>
	{
	}
}
