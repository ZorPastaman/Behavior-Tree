// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	public abstract class Behavior : IDisposable
	{
		private Blackboard m_blackboard;
		private Status m_status = Status.Idle;

		public static T Create<T>() where T : Behavior, new()
		{
			return new T();
		}

		public static Behavior Create([NotNull] Type behaviorType)
		{
			return (Behavior)Activator.CreateInstance(behaviorType);
		}

		public static Behavior Create([NotNull] Type behaviorType, params object[] parameters)
		{
			return (Behavior)Activator.CreateInstance(behaviorType, parameters);
		}

		public Status status
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_status;
		}

		protected Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual void Initialize() {}

		public Status Tick()
		{
			if (m_status != Status.Running)
			{
				Begin();
			}

			m_status = Execute();

			if (m_status != Status.Running)
			{
				End();
			}

			return m_status;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual void Dispose()
		{
			Abort();
		}

		public Status Abort()
		{
			if (m_status == Status.Running)
			{
				OnAbort();
				m_status = Status.Abort;
			}

			return m_status;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal virtual void ApplyBlackboard([NotNull] Blackboard blackboardToApply)
		{
			m_blackboard = blackboardToApply;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void Begin() {}

		protected abstract Status Execute();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void End() {}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected virtual void OnAbort() {}
	}
}
