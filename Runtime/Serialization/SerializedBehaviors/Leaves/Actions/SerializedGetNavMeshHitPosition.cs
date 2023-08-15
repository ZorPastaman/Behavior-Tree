﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Hit Property Name", 0), NameOverride("Position Property Name", 1)]
	[SearchGroup("Nav Mesh Hit")]
	public sealed class SerializedGetNavMeshHitPosition : SerializedAction<GetNavMeshHitPosition, string, string>
	{
	}
}