// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	[NameOverride("Duration Property Name", 0)]
	public sealed class SerializedLimitOfSecondsVariable : SerializedDecorator<LimitOfSecondsVariable, string>
	{
	}
}
