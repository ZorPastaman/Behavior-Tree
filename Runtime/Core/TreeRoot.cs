// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	/// <summary>
	/// Root of a behavior tree.
	/// </summary>
	public sealed class TreeRoot : IDisposable
	{
		/// <summary>
		/// Necessary define if it's possible to access a <see cref="Blackboard"/>
		/// of a behavior tree from different threads.
		/// </summary>
		public const string MultithreadingDefine = "BEHAVIOR_TREE_BLACKBOARD_MULTITHREADING";

		/// <summary>
		/// <see cref="Blackboard"/> of the behavior tree.
		/// </summary>
		[NotNull] private readonly Blackboard m_blackboard;
		/// <summary>
		/// Root <see cref="Behavior"/>.
		/// </summary>
		[NotNull] private readonly Behavior m_rootBehavior;

		/// <param name="blackboard"><see cref="Blackboard"/> of the behavior tree.</param>
		/// <param name="rootBehavior">Root <see cref="Behavior"/>.</param>
		public TreeRoot([NotNull] Blackboard blackboard, [NotNull] Behavior rootBehavior)
		{
			m_blackboard = blackboard;
			m_rootBehavior = rootBehavior;
			m_rootBehavior.SetBlackboard(m_blackboard);
		}

		/// <summary>
		/// <see cref="Blackboard"/> of the behavior tree.
		/// </summary>
		[NotNull]
		public Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		/// <summary>
		/// Initializes the behavior tree.
		/// </summary>
		/// <remarks>
		/// You must call it before a first tick.
		/// </remarks>
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

		/// <summary>
		/// Ticks the behavior tree and returns its result.
		/// </summary>
		/// <returns>Result of the tick.</returns>
		/// <remarks>
		/// You must call <see cref="Initialize"/> before a first tick.
		/// </remarks>
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

		/// <summary>
		/// Aborts a behavior tree if it's in <see cref="Status.Running"/> state.
		/// </summary>
		/// <returns>
		/// <para>
		/// New state (<see cref="Status.Abort"/>) if the behavior tree was in <see cref="Status.Running"/> state
		/// before the call.
		/// </para>
		/// <para>
		/// Current state if the behavior tree wasn't in <see cref="Status.Running"/> state before the call.
		/// </para>
		/// </returns>
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

		/// <summary>
		/// Disposes the behavior tree.
		/// </summary>
		/// <remarks>
		/// <para>You must call it before it's destroyed.</para>
		/// <para>The method automatically calls <see cref="Abort"/>.</para>
		/// </remarks>
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
