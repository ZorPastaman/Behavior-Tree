// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder
{
	/// <summary>
	/// Base class for a builder of <see cref="Leaf"/>.
	/// </summary>
	internal abstract class LeafBuilder : BehaviorBuilder
	{
		/// <summary>
		/// Builds a <see cref="Leaf"/> behavior.
		/// </summary>
		/// <returns>Built <see cref="Leaf"/>.</returns>
		[NotNull, Pure]
		public abstract Leaf Build();
	}
}
