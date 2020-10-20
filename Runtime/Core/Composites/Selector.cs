// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	[UsedImplicitly, Preserve]
	public sealed class Selector : Composite
	{
		private int m_currentChildIndex;

		public Selector([NotNull] Blackboard blackboard, [NotNull] Behavior[] children) : base(blackboard, children)
		{
		}

		protected override void Begin()
		{
			base.Begin();
			m_currentChildIndex = 0;
		}

		protected override Status Execute()
		{
			var childStatus = Status.Failure;

			for (int count = children.Length;
				m_currentChildIndex < count & childStatus == Status.Failure;
				++m_currentChildIndex)
			{
				childStatus = children[m_currentChildIndex].Tick();
			}

			--m_currentChildIndex;

			return childStatus;
		}
	}
}
