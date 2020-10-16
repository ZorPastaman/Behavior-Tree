// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	public sealed class Tree : IDisposable
	{
		[NotNull] private readonly Blackboard m_blackboard;
		[NotNull] private readonly Behavior m_root;

		public Tree([NotNull] Blackboard blackboard, [NotNull] Behavior root)
		{
			m_blackboard = blackboard;
			m_root = root;
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
			m_root.Initialize();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Tick()
		{
			return m_root.Tick();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			m_root.Dispose();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Abort()
		{
			return m_root.Abort();
		}
	}
}
