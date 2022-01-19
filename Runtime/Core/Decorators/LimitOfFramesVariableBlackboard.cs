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
	/// If the elapsed frames since begin exceed the variable duration,
	/// this <see cref="Decorator"/> ticks with <see cref="Status.Failure"/> and aborts its child.
	/// </para>
	/// <para>
	/// This <see cref="Decorator"/> uses a <see cref="Blackboard"/> property of type <see cref="int"/>
	/// as a frame counter.
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of a frame counter of type <see cref="int"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of duration of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class LimitOfFramesVariableBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_framePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_durationPropertyName;

		[BehaviorInfo] private int m_beginFrame;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName framePropertyName, BlackboardPropertyName durationPropertyName)
		{
			SetupInternal(framePropertyName, durationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string framePropertyName, string durationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(framePropertyName),
				new BlackboardPropertyName(durationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName framePropertyName,
			BlackboardPropertyName durationPropertyName)
		{
			m_framePropertyName = framePropertyName;
			m_durationPropertyName = durationPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			blackboard.TryGetStructValue(m_framePropertyName, out m_beginFrame);
		}

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_framePropertyName, out int frame) |
				!blackboard.TryGetStructValue(m_durationPropertyName, out int duration))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();
			bool isTimeOver = childStatus == Status.Running & (frame - m_beginFrame >= duration);

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
