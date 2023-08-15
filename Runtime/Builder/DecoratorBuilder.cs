// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder
{
	/// <summary>
	/// Base class for a builder of <see cref="Decorator"/>.
	/// </summary>
	internal abstract class DecoratorBuilder : BehaviorBuilder
	{
		/// <summary>
		/// Child index.
		/// </summary>
		private int m_childIndex = -1;

		/// <summary>
		/// Child index.
		/// </summary>
		/// <remarks>
		/// By default, it's -1.
		/// </remarks>
		public int childIndex
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_childIndex;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_childIndex = value;
		}

		/// <summary>
		/// Builds a <see cref="Decorator"/> behavior.
		/// </summary>
		/// <param name="child">Child behavior for the built <see cref="Decorator"/>.</param>
		/// <returns>Built <see cref="Decorator"/>.</returns>
		[NotNull, Pure]
		public abstract Decorator Build([NotNull] Behavior child);
	}
}
