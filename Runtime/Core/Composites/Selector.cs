// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;

namespace Zor.BehaviorTree.Core.Composites
{
	public sealed class Selector : Composite, INotSetupable
	{
		private int m_currentChildIndex;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
