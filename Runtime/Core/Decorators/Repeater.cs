// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// <para>
	/// This <see cref="Decorator"/> ticks its child and returns its result.
	/// </para>
	/// <para>
	/// This decorator returns <see cref="Status.Success"/> as <see cref="Status.Running"/> for set times.
	/// </para>
	/// <para>
	/// How many times this <see cref="Decorator"/> returns <see cref="Status.Running"/> instead of
	/// <see cref="Status.Success"/> is set in the setup method.
	/// </para>
	/// </summary>
	public sealed class Repeater : Decorator, ISetupable<uint>
	{
		[BehaviorInfo] private uint m_repeats;

		[BehaviorInfo] private uint m_currentRepeats;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<uint>.Setup(uint repeats)
		{
			m_repeats = repeats;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override Status Execute()
		{
			Status childStatus = child.Tick();

			return childStatus != Status.Success
				? childStatus
				: StateToStatusHelper.FinishedToStatus(++m_currentRepeats >= m_repeats);
		}
	}
}
