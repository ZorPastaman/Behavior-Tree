// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class Repeater : Decorator, ISetupable<uint>
	{
		[BehaviorInfo] private uint m_repeats;

		[BehaviorInfo] private uint m_currentRepeats;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<uint>.Setup(uint repeats)
		{
			m_repeats = repeats;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override Status Execute()
		{
			Status childStatus = child.Tick();

			return childStatus != Status.Success
				? childStatus
				: StateToStatusHelper.FinishedToStatus(++m_currentRepeats >= m_repeats);
		}
	}
}
