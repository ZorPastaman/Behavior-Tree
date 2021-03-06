﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetIsAnimatorMatchingTarget : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName animatorPropertyName, BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(animatorPropertyName, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string animatorPropertyName, string valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName),
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName,
			BlackboardPropertyName valuePropertyName)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_valuePropertyName = valuePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				bool value = animator.isMatchingTarget;
				blackboard.SetStructValue(m_valuePropertyName, value);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
