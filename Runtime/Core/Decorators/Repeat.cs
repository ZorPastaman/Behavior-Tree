// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class Repeat : Decorator
	{
		private readonly uint m_repeats;
		private uint m_currentRepeats;

		public Repeat([NotNull] Blackboard blackboard, [NotNull] Behavior child, uint repeats) : base(blackboard, child)
		{
			m_repeats = repeats;
		}

		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0;
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
