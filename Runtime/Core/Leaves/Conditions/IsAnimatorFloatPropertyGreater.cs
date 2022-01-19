// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsAnimatorFloatPropertyGreater : Condition,
		ISetupable<BlackboardPropertyName, int, float>, ISetupable<BlackboardPropertyName, string, float>,
		ISetupable<string, int, float>, ISetupable<string, string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_propertyId;
		[BehaviorInfo] private float m_value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int, float>.Setup(
			BlackboardPropertyName animatorPropertyName, int propertyId, float value)
		{
			SetupInternal(animatorPropertyName, propertyId, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string, float>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName, float value)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName), value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int, float>.Setup(string animatorPropertyName, int propertyId, float value)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float>.Setup(string animatorPropertyName, string propertyName, float value)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(propertyName), value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int propertyId, float value)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_propertyId = propertyId;
			m_value = value;
		}

		[Pure]
		protected override Status Execute()
		{
			return blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null
				? StateToStatusHelper.ConditionToStatus(animator.GetFloat(m_propertyId) > m_value)
				: Status.Error;
		}
	}
}
