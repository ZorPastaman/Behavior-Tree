// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Waits for fixed duration returning <see cref="Status.Running"/> in its tick.
	/// When the duration is expired, returns <see cref="Status.Success"/>.
	/// This wait uses a <see cref="Blackboard"/> property of type <see cref="float"/> as a time counter.
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
	/// 	<item>
	/// 		<term><see cref="Status.Error"/> </term>
	/// 		<description>if there's no data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of a time counter of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Duration in seconds of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class WaitForSecondsBlackboard : Action,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_timePropertyName;
		[BehaviorInfo] private float m_duration;

		[BehaviorInfo] private float m_beginTime;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName timePropertyName, float duration)
		{
			SetupInternal(timePropertyName, duration);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string timePropertyName, float duration)
		{
			SetupInternal(new BlackboardPropertyName(timePropertyName), duration);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName timePropertyName, float duration)
		{
			m_timePropertyName = timePropertyName;
			m_duration = duration;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			blackboard.TryGetStructValue(m_timePropertyName, out m_beginTime);
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasTime = blackboard.TryGetStructValue(m_timePropertyName, out float time);
			return StateToStatusHelper.FinishedToStatus(time - m_beginTime >= m_duration, hasTime);
		}
	}
}
