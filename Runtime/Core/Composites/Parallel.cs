// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Zor.BehaviorTree.Core.Composites
{
	[UsedImplicitly, Preserve]
	public sealed class Parallel : Composite
	{
		private int m_success;
		private int m_failures;
		private bool m_initialTick;

		private readonly Mode m_successMode;
		private readonly Mode m_failureMode;

		public Parallel(Mode mode)
		{
			m_successMode = mode;
			m_failureMode = mode;
		}

		public Parallel(Mode successMode, Mode failureMode)
		{
			m_successMode = successMode;
			m_failureMode = failureMode;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();

			m_success = 0;
			m_failures = 0;
			m_initialTick = true;
		}

		protected override Status Execute()
		{
			int childrenCount = children.Length;

			for (int i = 0; i < childrenCount; ++i)
			{
				Behavior child = children[i];

				if (!m_initialTick & child.status != Status.Running)
				{
					continue;
				}

				Status childStatus = child.Tick();

				if (childStatus == Status.Success)
				{
					if (m_successMode == Mode.Any)
					{
						return Status.Success;
					}

					++m_success;
				}
				else if (childStatus == Status.Failure)
				{
					if (m_failureMode == Mode.Any)
					{
						return Status.Failure;
					}

					++m_failures;
				}
				else if (childStatus != Status.Running)
				{
					return childStatus;
				}
			}

			if (m_successMode == Mode.All & m_success >= childrenCount)
			{
				return Status.Success;
			}

			if (m_failureMode == Mode.All & m_failures >= childrenCount)
			{
				return Status.Failure;
			}

			if (m_success > 0 & m_failures > 0)
			{
				return Status.Error;
			}

			m_initialTick = false;
			return Status.Running;
		}

		protected override void End()
		{
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Abort();
			}

			base.End();
		}

		[Flags]
		public enum Mode : byte
		{
			Any = 1 << 0,
			All = 1 << 1
		}
	}
}
