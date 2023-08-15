// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// <para>
	/// This <see cref="Decorator"/> ticks its child
	/// with a set cooldown duration after a tick with a <see cref="Status.Success"/> result.
	/// </para>
	/// <para>
	/// If this <see cref="Decorator"/> isn't in a cooldown, it ticks with a result of its child.
	/// If its child ticks with a <see cref="Status.Success"/> result, this <see cref="Decorator"/> starts a cooldown.
	/// In the cooldown state this <see cref="Decorator"/> ticks with a <see cref="Status.Failure"/> result.
	/// </para>
	/// <para>
	/// This <see cref="Decorator"/> uses <see cref="Time.time"/> as a time counter.
	/// </para>
	/// <para>
	/// The cooldown duration of type <see cref="float"/> is set in the setup method in seconds.
	/// </para>
	/// </summary>
	public sealed class CooldownOfSeconds : Decorator, ISetupable<float>
	{
		[BehaviorInfo] private float m_duration;

		[BehaviorInfo] private float m_lastChildTickTime;
		private bool m_isLastTickSuccess;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<float>.Setup(float duration)
		{
			m_duration = duration;
		}

		protected override Status Execute()
		{
			float time = Time.time;

			if (m_isLastTickSuccess & (time - m_lastChildTickTime < m_duration))
			{
				return Status.Failure;
			}

			Status childStatus = child.Tick();
			m_lastChildTickTime = time;
			m_isLastTickSuccess = childStatus == Status.Success;

			return childStatus;
		}
	}
}
