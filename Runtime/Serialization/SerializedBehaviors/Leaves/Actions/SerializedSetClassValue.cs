﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Value", 0), NameOverride("Property Name", 1)]
	[SearchGroup("Set Class Value")]
	public abstract class SerializedSetClassValue<T> : SerializedAction<SetClassValue<T>, T, string> where T : class
	{
	}
}
