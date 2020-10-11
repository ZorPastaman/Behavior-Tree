// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	public sealed class Sequence : Composite
	{
		private int m_currentChildIndex;

		public Sequence([NotNull] Blackboard blackboard, [NotNull] Behavior[] children) : base(blackboard, children)
		{
		}

		protected override void Begin()
		{
			base.Begin();
			m_currentChildIndex = 0;
		}

		protected override Status Execute()
		{
			for (int count = children.Length; m_currentChildIndex < count; ++m_currentChildIndex)
			{
				Status childStatus = children[m_currentChildIndex].Tick();

				if (childStatus != Status.Success)
				{
					return childStatus;
				}
			}

			return Status.Success;
		}
	}
}
