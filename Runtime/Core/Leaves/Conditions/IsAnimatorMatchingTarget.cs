// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsAnimatorMatchingTarget : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName animatorPropertyName)
		{
			SetupInternal(animatorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string animatorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName)
		{
			m_animatorPropertyName = animatorPropertyName;
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				Status* results = stackalloc Status[] {Status.Failure, Status.Success};
				bool isMatching = animator.isMatchingTarget;
				byte index = *(byte*)&isMatching;

				return results[index];
			}

			return Status.Error;
		}
	}
}
