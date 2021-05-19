// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
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

		protected override unsafe Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_framePropertyName, out int frame) |
				!blackboard.TryGetStructValue(m_durationPropertyName, out int duration))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();
			Status* results = stackalloc[] {childStatus, Status.Failure};
			bool isTimeOver = childStatus == Status.Running & (frame - m_beginFrame >= duration);
			byte index = *(byte*)&isTimeOver;

			return results[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void End()
		{
			child.Abort();
			base.End();
		}
	}
}
