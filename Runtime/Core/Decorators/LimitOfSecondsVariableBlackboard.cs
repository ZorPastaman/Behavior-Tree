// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// <para>
	/// This <see cref="Decorator"/> ticks its child and returns its result
	/// but it allows to be in <see cref="Status.Running"/> state for a variable duration.
	/// </para>
	/// <para>
	/// If the elapsed time since begin exceeds the variable duration,
	/// this <see cref="Decorator"/> ticks with <see cref="Status.Failure"/> and aborts its child.
	/// </para>
	/// <para>
	/// This <see cref="Decorator"/> uses a <see cref="Blackboard"/> property of type <see cref="float"/>
	/// as a time counter.
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
	/// 		<description>Property name of a duration of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class LimitOfSecondsVariableBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_timePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_durationPropertyName;

		[BehaviorInfo] private float m_beginTime;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName timePropertyName, BlackboardPropertyName durationPropertyName)
		{
			SetupInternal(timePropertyName, durationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string timePropertyName, string durationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(timePropertyName),
				new BlackboardPropertyName(durationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName timePropertyName, BlackboardPropertyName durationPropertyName)
		{
			m_timePropertyName = timePropertyName;
			m_durationPropertyName = durationPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			blackboard.TryGetStructValue(m_timePropertyName, out m_beginTime);
		}

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_timePropertyName, out float time) |
				!blackboard.TryGetStructValue(m_durationPropertyName, out float duration))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();
			bool isTimeOver = childStatus == Status.Running & (time - m_beginTime >= duration);

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
