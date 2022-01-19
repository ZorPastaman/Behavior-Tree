﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	[NameOverride("Frame Property Name", 0), NameOverride("Duration", 1)]
	[SearchGroup("Cooldowns")]
	public sealed class SerializedCooldownOfFramesBlackboard :
		SerializedDecorator<CooldownOfFramesBlackboard, string, int>
	{
	}
}
