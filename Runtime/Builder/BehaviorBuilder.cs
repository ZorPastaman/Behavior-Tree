// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Builder
{
	/// <summary>
	/// Base class for behavior builders.
	/// </summary>
	/// <remarks>
	/// Don't derive it. Derive <see cref="CompositeBuilder"/>, <see cref="DecoratorBuilder"/> or <see cref="LeafBuilder"/>.
	/// </remarks>
	internal abstract class BehaviorBuilder
	{
		/// <summary>
		/// Built type.
		/// </summary>
		[NotNull]
		public abstract Type behaviorType { [Pure] get; }
	}
}
