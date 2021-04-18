// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder
{
	internal abstract class LeafBuilder : BehaviorBuilder
	{
		[NotNull, Pure]
		public abstract Leaf Build();
	}
}
