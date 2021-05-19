﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	[NameOverride("Frame Property Name", 0), NameOverride("Duration", 1)]
	[SearchGroup("Limits")]
	public sealed class SerializedLimitOfFramesBlackboard : SerializedDecorator<LimitOfFramesBlackboard, string, int>
	{
	}
}
