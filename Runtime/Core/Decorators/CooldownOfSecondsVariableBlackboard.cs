// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class CooldownOfSecondsVariableBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_timePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_durationPropertyName;

		[BehaviorInfo] private float m_lastChildTickTime;
		private bool m_isLastTickSuccess;

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

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_timePropertyName, out float time) |
				!blackboard.TryGetStructValue(m_durationPropertyName, out float duration))
			{
				return Status.Error;
			}

			if (m_isLastTickSuccess & (time - m_lastChildTickTime < duration))
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
