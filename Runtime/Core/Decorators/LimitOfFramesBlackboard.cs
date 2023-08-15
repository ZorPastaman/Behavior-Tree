// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

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
	/// 		<description>Duration of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class LimitOfFramesBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, int>, ISetupable<string, int>
	{
		[BehaviorInfo] private BlackboardPropertyName m_framePropertyName;
		[BehaviorInfo] private int m_duration;

		[BehaviorInfo] private int m_beginFrame;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int>.Setup(BlackboardPropertyName framePropertyName, int duration)
		{
			SetupInternal(framePropertyName, duration);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int>.Setup(string framePropertyName, int duration)
		{
			SetupInternal(new BlackboardPropertyName(framePropertyName), duration);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName framePropertyName, int duration)
		{
			m_framePropertyName = framePropertyName;
			m_duration = duration;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			blackboard.TryGetStructValue(m_framePropertyName, out m_beginFrame);
		}

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_framePropertyName, out int frame))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();
			bool isTimeOver = childStatus == Status.Running & (frame - m_beginFrame >= m_duration);

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
