﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Angle Property Name", 0), NameOverride("Axis Property Name", 1),
	NameOverride("Quaternion Property Name", 2)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedQuaternionAngleAxis : SerializedAction<QuaternionAngleAxis, string, string, string>
	{
	}
}
