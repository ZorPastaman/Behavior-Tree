﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Source Property Name", 0), NameOverride("Max Distance", 1),
	NameOverride("Hit Property Name", 2)]
	[SearchGroup("Nav Mesh")]
	public sealed class SerializedNavMeshSamplePosition : SerializedAction<NavMeshSamplePosition, string, float, string>
	{
	}
}
