// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder
{
	internal abstract class DecoratorBuilder : BehaviorBuilder
	{
		private int m_childIndex;

		public int childIndex
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_childIndex;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_childIndex = value;
		}

		[NotNull, Pure]
		public abstract Decorator Build([NotNull] Behavior child);
	}
}
