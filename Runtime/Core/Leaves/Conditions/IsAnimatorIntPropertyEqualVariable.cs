// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsAnimatorIntPropertyEqualVariable : Condition,
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
		protected override unsafe Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null &
				blackboard.TryGetStructValue(m_valuePropertyName, out int value))
			{
				Status* results = stackalloc Status[] {Status.Failure, Status.Success};
				bool equals = animator.GetInteger(m_propertyId) == value;
				byte index = *(byte*)&equals;

				return results[index];
			}

			return Status.Error;
		}
	}
}
