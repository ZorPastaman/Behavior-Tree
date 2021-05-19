// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
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
