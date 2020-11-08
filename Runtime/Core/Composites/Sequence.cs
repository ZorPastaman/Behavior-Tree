// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Zor.BehaviorTree.Core.Composites
{
	[UsedImplicitly, Preserve]
	public sealed class Sequence : Composite
	{
		private int m_currentChildIndex;

		public Sequence([NotNull] Behavior[] children) : base(children)
		{
		}

		protected override void Begin()
		{
			base.Begin();
			m_currentChildIndex = 0;
		}

		protected override Status Execute()
		{
			var childStatus = Status.Success;

			for (int count = children.Length;
				m_currentChildIndex < count & childStatus == Status.Success;
				++m_currentChildIndex)
			{
				childStatus = children[m_currentChildIndex].Tick();
			}

			--m_currentChildIndex;

			return childStatus;
		}
	}
}
