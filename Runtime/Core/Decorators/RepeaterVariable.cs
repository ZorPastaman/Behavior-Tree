// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// <para>
	/// This <see cref="Decorator"/> ticks its child and returns its result.
	/// </para>
	/// <para>
	/// This decorator returns <see cref="Status.Success"/> as <see cref="Status.Running"/> for variable times.
	/// </para>
	/// <para>
	/// The property of how many times this <see cref="Decorator"/> returns <see cref="Status.Running"/> instead of
	/// <see cref="Status.Success"/> is set in the setup method. The property type is <see cref="uint"/>.
	/// </para>
	/// </summary>
	public sealed class RepeaterVariable : Decorator, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_repeatsPropertyName;

		[BehaviorInfo] private uint m_currentRepeats;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName repeatsPropertyName)
		{
			SetupInternal(repeatsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string repeatsPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(repeatsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName repeatsPropertyName)
		{
			m_repeatsPropertyName = repeatsPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void Begin()
		{
			base.Begin();
			m_currentRepeats = 0u;
		}

		protected override Status Execute()
		{
			if (!blackboard.TryGetStructValue(m_repeatsPropertyName, out uint repeats))
			{
				return Status.Error;
			}

			Status childStatus = child.Tick();

			return childStatus != Status.Success
				? childStatus
				: StateToStatusHelper.FinishedToStatus(++m_currentRepeats >= repeats);
		}
	}
}
