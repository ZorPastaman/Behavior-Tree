// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Zor.BehaviorTree.Core.Decorators
{
	[UsedImplicitly, Preserve]
	public sealed class Repeat : Decorator
	{
		private readonly uint m_repeats;
		private uint m_currentRepeats;

		public Repeat(uint repeats)
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

			if (childStatus == Status.Success)
			{
				Status* results = stackalloc Status[] {Status.Running, Status.Success};
				bool finished = ++m_currentRepeats >= m_repeats;
				byte index = *(byte*)&finished;
				childStatus = results[index];
			}

			return childStatus;
		}
	}
}
