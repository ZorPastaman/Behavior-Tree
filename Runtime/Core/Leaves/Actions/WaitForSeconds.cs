// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Waits for fixed duration returning <see cref="Status.Running"/> in its tick.
	/// When the duration is expired, returns <see cref="Status.Success"/>.
	/// This wait uses <see cref="Time.time"/> as a time counter.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>when the duration is expired.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Running"/> </term>
	/// 		<description>when the duration isn't expired.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Duration in seconds of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class WaitForSeconds : Action, ISetupable<float>
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

		[Pure]
		protected override Status Execute()
		{
			return StateToStatusHelper.FinishedToStatus(Time.time - m_beginTime >= m_duration);
		}
	}
}
