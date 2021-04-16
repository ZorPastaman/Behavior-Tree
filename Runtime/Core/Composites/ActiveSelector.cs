// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;

namespace Zor.BehaviorTree.Core.Composites
{
	public sealed class ActiveSelector : Composite, INotSetupable
	{
		private int m_currentChildIndex;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentChildIndex = -1;
		}

		protected override Status Execute()
		{
			int childIndex = 0;
			var childStatus = Status.Failure;

			for (int count = children.Length; childIndex < count & childStatus == Status.Failure; ++childIndex)
			{
				childStatus = children[childIndex].Tick();
			}

			--childIndex;

			if (m_currentChildIndex > childIndex)
			{
				children[m_currentChildIndex].Abort();
			}

			m_currentChildIndex = childIndex;

			return childStatus;
		}
	}
}
