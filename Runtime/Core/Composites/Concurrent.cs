// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Composites
{
	/// <summary>
	/// <para>
	/// This <see cref="Composite"/> ticks all its children in its tick. Returned <see cref="Status"/> rules depend
	/// on set <see cref="Mode"/>. The <see cref="Mode"/> may be set the same or different for
	/// <see cref="Status.Success"/> and <see cref="Status.Failure"/> rules.
	/// </para>
	/// <para>
	/// Statuses <see cref="Status.Success"/> and <see cref="Status.Failure"/> in this <see cref="Composite"/> tick are
	/// dependent on their <see cref="Mode"/>.
	/// If none of the <see cref="Mode"/> rules is triggered,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Running"/>.
	/// If any child ticks with <see cref="Status.Error"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Error"/> too.
	/// </para>
	/// <para>
	/// Every end, this <see cref="Composite"/> aborts all its children.
	/// </para>
	/// </summary>
	/// <seealso cref="Parallel"/>
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

		/// <summary>
		/// <see cref="Status.Success"/> and <see cref="Status.Failure"/> rule.
		/// </summary>
		[Flags]
		public enum Mode : byte
		{
			/// <summary>
			/// At least one child ticking with <see cref="Status.Success"/> or <see cref="Status.Failure"/>
			/// is required.
			/// </summary>
			Any = 1 << 0,
			/// <summary>
			/// All the children ticking with <see cref="Status.Success"/> or <see cref="Status.Failure"/> are required.
			/// </summary>
			All = 1 << 1
		}
	}
}
