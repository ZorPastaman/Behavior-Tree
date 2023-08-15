// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	[NameOverride("Duration", 0)]
	[SearchGroup("Limits")]
	public sealed class SerializedLimitOfSeconds : SerializedDecorator<LimitOfSeconds, float>
	{
	}
}
