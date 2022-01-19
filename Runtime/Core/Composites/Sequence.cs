// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;

namespace Zor.BehaviorTree.Core.Composites
{
	/// <summary>
	/// <para>
	/// This <see cref="Composite"/> ticks all the children in sequence.
	/// </para>
	/// <para>
	/// If a child ticks with <see cref="Status.Success"/>, this <see cref="Composite"/> ticks a next child.
	/// If a current child ticks with <see cref="Status.Running"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Running"/> too.
	/// If a current child ticks with <see cref="Status.Failure"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Failure"/> too.
	/// If a current child ticks with <see cref="Status.Error"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Error"/> too.
	/// If all the children tick with <see cref="Status.Success"/>,
	/// this <see cref="Composite"/> ticks with <see cref="Status.Success"/> too.
	/// </para>
	/// <para>
	/// Every begin, this <see cref="Composite"/> starts from its first child.
	/// </para>
	/// </summary>
	public sealed class Sequence : Composite, INotSetupable
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
