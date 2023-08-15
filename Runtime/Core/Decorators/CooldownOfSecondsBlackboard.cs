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
	/// 		<description>Cooldown duration of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class CooldownOfSecondsBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_timePropertyName;
		[BehaviorInfo] private float m_duration;

		[BehaviorInfo] private float m_lastChildTickTime;
		private bool m_isLastTickSuccess;

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

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_timePropertyName, out float time))
			{
				return Status.Error;
			}

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
