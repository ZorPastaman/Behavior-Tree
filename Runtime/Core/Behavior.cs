// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core
{
	public abstract class Behavior : IDisposable
	{
		private readonly Blackboard m_blackboard;
		private Status m_status = Status.Idle;

		protected Behavior([NotNull] Blackboard blackboard)
		{
			m_blackboard = blackboard;
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

		protected virtual void Begin() {}
		protected abstract Status Execute();
		protected virtual void End() {}
		protected virtual void OnAbort() {}
	}
}
