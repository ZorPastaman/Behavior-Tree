// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Builder
{
	internal abstract class CompositeBuilder : BehaviorBuilder
	{
		private readonly List<int> m_children = new List<int>();

		public int childCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_children.Count;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public int GetChildIndex(int index)
		{
			return m_children[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddChildIndex(int index)
		{
			m_children.Add(index);
		}

		[NotNull, Pure]
		public abstract Composite Build([NotNull, ItemNotNull] Behavior[] children);
	}
}
