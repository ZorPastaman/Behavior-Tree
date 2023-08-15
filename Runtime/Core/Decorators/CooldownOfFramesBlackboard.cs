// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

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
	/// 		<description>Cooldown duration of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class CooldownOfFramesBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, int>, ISetupable<string, int>
	{
		[BehaviorInfo] private BlackboardPropertyName m_framePropertyName;
		[BehaviorInfo] private int m_duration;

		[BehaviorInfo] private int m_lastChildTickFrame;
		private bool m_isLastTickSuccess;

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

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_framePropertyName, out int frame))
			{
				return Status.Error;
			}

			if (m_isLastTickSuccess & (frame - m_lastChildTickFrame < m_duration))
			{
				return Status.Failure;
			}

			Status childStatus = child.Tick();
			m_lastChildTickFrame = frame;
			m_isLastTickSuccess = childStatus == Status.Success;

			return childStatus;
		}
	}
}
