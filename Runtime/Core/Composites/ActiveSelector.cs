// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	[UsedImplicitly, Preserve]
	public sealed class ActiveSelector : Composite
	{
		private int m_currentChildIndex;

		public ActiveSelector([NotNull] Blackboard blackboard, [NotNull] Behavior[] children) : base(blackboard, children)
		{
		}

		protected override void Begin()
		{
			base.Begin();
			m_currentChildIndex = -1;
		}

		protected override Status Execute()
		{
			int childIndex = 0;
			Status childStatus = Status.Failure;

			for (int count = children.Length; childIndex < count; ++childIndex)
			{
				childStatus = children[childIndex].Tick();

				if (childStatus != Status.Failure)
				{
					break;
				}
			}

			if (m_currentChildIndex >= 0 & m_currentChildIndex != childIndex)
			{
				children[m_currentChildIndex].Abort();
			}

			m_currentChildIndex = childIndex;

			return childStatus;
		}
	}
}
