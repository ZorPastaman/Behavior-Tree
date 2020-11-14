// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	public sealed class TreeRoot : IDisposable
	{
		[NotNull] private readonly Blackboard m_blackboard;
		[NotNull] private readonly Behavior m_rootBehavior;

		public TreeRoot([NotNull] Blackboard blackboard, [NotNull] Behavior rootBehavior)
		{
			m_blackboard = blackboard;
			m_rootBehavior = rootBehavior;
			m_rootBehavior.ApplyBlackboard(m_blackboard);
		}

		[NotNull]
		public Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Initialize()
		{
			m_rootBehavior.Initialize();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Tick()
		{
			return m_rootBehavior.Tick();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			m_rootBehavior.Dispose();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Abort()
		{
			return m_rootBehavior.Abort();
		}
	}
}
