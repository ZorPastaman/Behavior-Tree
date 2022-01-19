// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// <para>
	/// This <see cref="Decorator"/> ticks its child and returns its result
	/// but it allows to be in <see cref="Status.Running"/> state for a set duration.
	/// </para>
	/// <para>
	/// If the elapsed time since begin exceeds the set duration,
	/// this <see cref="Decorator"/> ticks with <see cref="Status.Failure"/> and aborts its child.
	/// </para>
	/// <para>
	/// This <see cref="Decorator"/> uses <see cref="Time.time"/> as a time counter.
	/// </para>
	/// <para>
	/// The duration of type <see cref="float"/> is set in the setup method in seconds.
	/// </para>
	/// </summary>
	public sealed class LimitOfSeconds : Decorator, ISetupable<float>
	{
		[BehaviorInfo] private float m_duration;

		[BehaviorInfo] private float m_beginTime;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<float>.Setup(float duration)
		{
			m_duration = duration;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_beginTime = Time.time;
		}

		protected override Status Execute()
		{
			Status childStatus = child.Tick();
			bool isTimeOver = childStatus == Status.Running & (Time.time - m_beginTime >= m_duration);

			return StateToStatusHelper.ConditionToStatus(isTimeOver, childStatus, Status.Failure);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void End()
		{
			child.Abort();
			base.End();
		}
	}
}
