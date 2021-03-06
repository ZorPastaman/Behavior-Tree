﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	[NameOverride("Repeats Property Name", 0)]
	[SearchGroup("Repeaters")]
	public sealed class SerializedRepeaterVariable : SerializedDecorator<RepeaterVariable, string>
	{
	}
}
