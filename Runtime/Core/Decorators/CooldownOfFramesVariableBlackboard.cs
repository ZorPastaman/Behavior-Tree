// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class CooldownOfFramesVariableBlackboard : Decorator,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_framePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_durationPropertyName;

		[BehaviorInfo] private int m_lastChildTickFrame;
		private bool m_isLastTickSuccess;

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

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_framePropertyName, out int frame) |
				!blackboard.TryGetStructValue(m_durationPropertyName, out int duration))
			{
				return Status.Error;
			}

			if (m_isLastTickSuccess & (frame - m_lastChildTickFrame < duration))
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
