// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class PlayAnimatorState : Action,
		ISetupable<BlackboardPropertyName, int, int>, ISetupable<BlackboardPropertyName, string, int>,
		ISetupable<string, int, int>, ISetupable<string, string, int>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_stateId;
		[BehaviorInfo] private int m_layer;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int, int>.Setup(BlackboardPropertyName animatorPropertyName,
			int stateId, int layer)
		{
			SetupInternal(animatorPropertyName, stateId, layer);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string, int>.Setup(BlackboardPropertyName animatorPropertyName,
			string stateName, int layer)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(stateName), layer);
		}

		void ISetupable<string, int, int>.Setup(string animatorPropertyName, int stateId, int layer)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), stateId, layer);
		}

		void ISetupable<string, string, int>.Setup(string animatorPropertyName, string stateName, int layer)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(stateName), layer);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int stateId, int layer)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_stateId = stateId;
			m_layer = layer;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				animator.Play(m_stateId, m_layer);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
