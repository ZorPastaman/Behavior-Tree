// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
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
			Profiler.BeginSample("TreeRoot.Initialize");

#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				m_rootBehavior.Initialize();
			}

			Profiler.EndSample();
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public Status Tick()
		{
			Profiler.BeginSample("TreeRoot.Tick");

			Status status;

#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				status = m_rootBehavior.Tick();
			}

			Profiler.EndSample();

			return status;
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public Status Abort()
		{
			Profiler.BeginSample("TreeRoot.Abort");

			Status status;

#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				status = m_rootBehavior.Abort();
			}

			Profiler.EndSample();

			return status;
		}

#if !BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public void Dispose()
		{
			Profiler.BeginSample("TreeRoot.Dispose call");

#if BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING
			lock (m_blackboard)
#endif
			{
				Profiler.BeginSample("TreeRoot.Abort");
				m_rootBehavior.Abort();
				Profiler.EndSample();

				Profiler.BeginSample("TreeRoot.Dispose");
				m_rootBehavior.Dispose();
				Profiler.EndSample();
			}

			Profiler.EndSample();
		}
	}
}
