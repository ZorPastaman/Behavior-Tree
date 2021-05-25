// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetAnimatorTriggerProperty : Action,
		ISetupable<BlackboardPropertyName, int>, ISetupable<BlackboardPropertyName, string>,
		ISetupable<string, int>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_propertyId;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int>.Setup(
			BlackboardPropertyName animatorPropertyName, int propertyId)
		{
			SetupInternal(animatorPropertyName, propertyId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int>.Setup(string animatorPropertyName, int propertyId)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string animatorPropertyName, string propertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int propertyId)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_propertyId = propertyId;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				animator.SetTrigger(m_propertyId);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
