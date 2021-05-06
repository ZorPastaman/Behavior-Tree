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
		public void Setup(BlackboardPropertyName repeatsPropertyName)
		{
			m_repeatsPropertyName = repeatsPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(string repeatsPropertyName)
		{
			Setup(new BlackboardPropertyName(repeatsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override unsafe Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_repeatsPropertyName, out uint repeats))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();

			if (childStatus != Status.Success)
			{
				return childStatus;
			}

			Status* results = stackalloc Status[] {Status.Running, Status.Success};
			bool finished = ++m_currentRepeats >= repeats;
			byte index = *(byte*)&finished;

			return results[index];
		}
	}
}
