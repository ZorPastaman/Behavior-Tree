// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Zor.BehaviorTree.Core.Decorators
{
	[UsedImplicitly, Preserve]
	public sealed class Repeat : Decorator
	{
		private readonly uint m_repeats;
		private uint m_currentRepeats;

		public Repeat([NotNull] Behavior child, uint repeats) : base(child)
		{
			m_repeats = repeats;
		}

		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override Status Execute()
		{
			Status childStatus = child.Tick();

			if (childStatus == Status.Success)
			{
				return ++m_currentRepeats >= m_repeats ? Status.Success : Status.Running;
			}

			return childStatus;
		}
	}
}
