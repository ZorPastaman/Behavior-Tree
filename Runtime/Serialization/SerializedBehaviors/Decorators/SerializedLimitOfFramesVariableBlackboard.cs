﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	[NameOverride("Frame Property Name", 0), NameOverride("Duration Property Name", 1)]
	[SearchGroup("Limits")]
	public sealed class SerializedLimitOfFramesVariableBlackboard :
		SerializedDecorator<LimitOfFramesVariableBlackboard, string, string>
	{
	}
}
