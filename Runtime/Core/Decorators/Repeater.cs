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
		public void Setup(uint repeats)
		{
			m_repeats = repeats;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();

			if (childStatus != Status.Success)
			{
				return childStatus;
			}

			Status* results = stackalloc Status[] {Status.Running, Status.Success};
			bool finished = ++m_currentRepeats >= m_repeats;
			byte index = *(byte*)&finished;

			return results[index];
		}
	}
}
