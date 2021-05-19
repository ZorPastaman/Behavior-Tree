// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	public sealed class TreeRoot : IDisposable
	{
		public const string MultithreadingDefine = "BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING";

		[NotNull] private readonly Blackboard m_blackboard;
		[NotNull] private readonly Behavior m_rootBehavior;

		public TreeRoot([NotNull] Blackboard blackboard, [NotNull] Behavior rootBehavior)
		{
			m_blackboard = blackboard;
			m_rootBehavior = rootBehavior;
			m_rootBehavior.SetBlackboard(m_blackboard);
		}

		[NotNull]
		public Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void Initialize()
		{
#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				m_rootBehavior.Initialize();
			}
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public Status Tick()
		{
#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				return m_rootBehavior.Tick();
			}
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public Status Abort()
		{
#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				return m_rootBehavior.Abort();
			}
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void Dispose()
		{
#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				m_rootBehavior.Abort();
				m_rootBehavior.Dispose();
			}
		}
	}
}
