// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsAnimatorFloatPropertyGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, int, BlackboardPropertyName>,
		ISetupable<BlackboardPropertyName, string, string>,
		ISetupable<string, int, BlackboardPropertyName>, ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_propertyId;
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int, BlackboardPropertyName>.Setup(
			BlackboardPropertyName animatorPropertyName, int propertyId, BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(animatorPropertyName, propertyId, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string, string>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName, string valuePropertyName)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName),
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int, BlackboardPropertyName>.Setup(string animatorPropertyName, int propertyId,
			BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string animatorPropertyName, string propertyName, string valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(propertyName),
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int propertyId,
			BlackboardPropertyName valuePropertyName)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_propertyId = propertyId;
			m_valuePropertyName = valuePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			return blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null &
				blackboard.TryGetStructValue(m_valuePropertyName, out float value)
					? StateToStatusHelper.ConditionToStatus(animator.GetFloat(m_propertyId) > value)
					: Status.Error;
		}
	}
}
