// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Composites
{
	public sealed class Concurrent : Composite,
		ISetupable<Concurrent.Mode>, ISetupable<Concurrent.Mode, Concurrent.Mode>
	{
		[BehaviorInfo] private Mode m_successMode;
		[BehaviorInfo] private Mode m_failureMode;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<Mode>.Setup(Mode mode)
		{
			SetupInternal(mode, mode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<Mode, Mode>.Setup(Mode successMode, Mode failureMode)
		{
			SetupInternal(successMode, failureMode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(Mode successMode, Mode failureMode)
		{
			m_successMode = successMode;
			m_failureMode = failureMode;
		}

		protected override Status Execute()
		{
			int successes = 0;
			int failures = 0;
			int childrenCount = children.Length;

			for (int i = 0; i < childrenCount; ++i)
			{
				Status childStatus = children[i].Tick();

				if (childStatus == Status.Success)
				{
					if (m_successMode == Mode.Any)
					{
						return Status.Success;
					}

					++successes;
				}
				else if (childStatus == Status.Failure)
				{
					if (m_failureMode == Mode.Any)
					{
						return Status.Failure;
					}

					++failures;
				}
				else if (childStatus != Status.Running)
				{
					return childStatus;
				}
			}

			if (m_successMode == Mode.All & successes >= childrenCount)
			{
				return Status.Success;
			}

			if (m_failureMode == Mode.All & failures >= childrenCount)
			{
				return Status.Failure;
			}

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
