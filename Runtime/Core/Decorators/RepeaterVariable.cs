// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class RepeaterVariable : Decorator, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_repeatsPropertyName;

		[BehaviorInfo] private uint m_currentRepeats;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName repeatsPropertyName)
		{
			SetupInternal(repeatsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string repeatsPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(repeatsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName repeatsPropertyName)
		{
			m_repeatsPropertyName = repeatsPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_repeatsPropertyName, out uint repeats))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();

			return childStatus != Status.Success
				? childStatus
				: StateToStatusHelper.FinishedToStatus(++m_currentRepeats >= repeats);
		}
	}
}
