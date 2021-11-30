// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Builder
{
	internal abstract class BehaviorBuilder
	{
		[NotNull]
		public abstract Type behaviorType { [Pure] get; }
	}
}
