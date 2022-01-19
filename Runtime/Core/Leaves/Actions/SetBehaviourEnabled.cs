// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetBehaviourEnabled : Action, ISetupable<BlackboardPropertyName, bool>, ISetupable<string, bool>
	{
		[BehaviorInfo] private BlackboardPropertyName m_behaviourPropertyName;
		private bool m_enabled;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool>.Setup(BlackboardPropertyName behaviourPropertyName, bool enabled)
		{
			SetupInternal(behaviourPropertyName, enabled);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool>.Setup(string behaviourPropertyName, bool enabled)
		{
			SetupInternal(new BlackboardPropertyName(behaviourPropertyName), enabled);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName behaviourPropertyName, bool enabled)
		{
			m_behaviourPropertyName = behaviourPropertyName;
			m_enabled = enabled;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_behaviourPropertyName, out Behaviour behaviour) & behaviour != null)
			{
				behaviour.enabled = m_enabled;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
