﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Duration Property Name", 0)]
	[SearchGroup("Waits")]
	public sealed class SerializedWaitForSecondsVariable : SerializedAction<WaitForSecondsVariable, string>
	{
	}
}
