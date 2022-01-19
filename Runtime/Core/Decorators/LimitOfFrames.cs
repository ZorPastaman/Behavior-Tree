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
	/// If the elapsed frames since begin exceed the set duration,
	/// this <see cref="Decorator"/> ticks with <see cref="Status.Failure"/> and aborts its child.
	/// </para>
	/// <para>
	/// This <see cref="Decorator"/> uses <see cref="Time.frameCount"/> as a frame counter.
	/// </para>
	/// <para>
	/// The duration of type <see cref="int"/> is set in the setup method in frames.
	/// </para>
	/// </summary>
	public sealed class LimitOfFrames : Decorator, ISetupable<int>
	{
		[BehaviorInfo] private int m_duration;

		[BehaviorInfo] private int m_beginFrame;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<int>.Setup(int duration)
		{
			m_duration = duration;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_beginFrame = Time.frameCount;
		}

		protected override Status Execute()
		{
			Status childStatus = child.Tick();
			bool isTimeOver = childStatus == Status.Running & (Time.frameCount - m_beginFrame >= m_duration);

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
