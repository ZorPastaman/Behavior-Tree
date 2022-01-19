// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;

namespace Zor.BehaviorTree.Core.Composites
{
	/// <summary>
	/// <para>
	/// This <see cref="Composite"/> ticks all the children from the first every its tick.
	/// If a child ticks with any <see cref="Status"/> except <see cref="Status.Failure"/>,
	/// it becomes a current child behavior and a previous current child behavior is aborted.
	/// </para>
	/// <para>
	/// If a current child ticks with <see cref="Status.Success"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Success"/> too.
	/// If a current child ticks with <see cref="Status.Running"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Running"/> too.
	/// If a current child ticks with <see cref="Status.Failure"/>, this <see cref="Composite"/> ticks a next child.
	/// If a current child ticks with <see cref="Status.Error"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Error"/> too.
	/// If all the children tick with <see cref="Status.Failure"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Failure"/> too.
	/// </para>
	/// <para>
	/// Every begin, this <see cref="Composite"/> forgets a current child behavior.
	/// </para>
	/// </summary>
	/// <seealso cref="Selector"/>
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
