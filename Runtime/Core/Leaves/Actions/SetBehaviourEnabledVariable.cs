// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetBehaviourEnabledVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_behaviourPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_enabledPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName behaviourPropertyName, BlackboardPropertyName enabledPropertyName)
		{
			SetupInternal(behaviourPropertyName, enabledPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string behaviourPropertyName, string enabledPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(behaviourPropertyName),
				new BlackboardPropertyName(enabledPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName behaviourPropertyName,
			BlackboardPropertyName enabledPropertyName)
		{
			m_behaviourPropertyName = behaviourPropertyName;
			m_enabledPropertyName = enabledPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_behaviourPropertyName, out Behaviour behaviour) & behaviour != null &
				blackboard.TryGetStructValue(m_enabledPropertyName, out bool enabled))
			{
				behaviour.enabled = enabled;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
