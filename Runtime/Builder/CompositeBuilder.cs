// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Builder
{
	/// <summary>
	/// Base class for a builder of <see cref="Composite"/>.
	/// </summary>
	internal abstract class CompositeBuilder : BehaviorBuilder
	{
		/// <summary>
		/// Children indices.
		/// </summary>
		private readonly List<int> m_children = new List<int>();

		/// <summary>
		/// How many children are added to a built <see cref="Composite"/>.
		/// </summary>
		public int childCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_children.Count;
		}

		/// <summary>
		/// Gets a child index by the ordinal <paramref name="index"/>.
		/// </summary>
		/// <param name="index">Ordinal index.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public int GetChildIndex(int index)
		{
			return m_children[index];
		}

		/// <summary>
		/// Adds a child <paramref name="index"/>.
		/// </summary>
		/// <param name="index">Child index.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddChildIndex(int index)
		{
			m_children.Add(index);
		}

		/// <summary>
		/// Builds a <see cref="Composite"/> behavior.
		/// </summary>
		/// <param name="children">Children behaviors for the built <see cref="Composite"/>.</param>
		/// <returns>Built <see cref="Composite"/>.</returns>
		[NotNull, Pure]
		public abstract Composite Build([NotNull, ItemNotNull] Behavior[] children);
	}
}
