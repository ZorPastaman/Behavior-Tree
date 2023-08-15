﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("Number", 1), NameOverride("Result Property Name", 2)]
	[SearchGroup("Vector3")]
	public sealed class SerializedDivideVector3AndNumber :
		SerializedAction<DivideVector3AndNumber, string, float, string>
	{
	}
}
