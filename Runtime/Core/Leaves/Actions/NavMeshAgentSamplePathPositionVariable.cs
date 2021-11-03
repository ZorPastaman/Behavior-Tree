// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class NavMeshAgentSamplePathPositionVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_areaMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maxDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName agentPropertyName, BlackboardPropertyName areaMaskPropertyName,
				BlackboardPropertyName maxDistancePropertyName, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(agentPropertyName, areaMaskPropertyName, maxDistancePropertyName, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string agentPropertyName, string areaMaskPropertyName,
			string maxDistancePropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(areaMaskPropertyName), new BlackboardPropertyName(maxDistancePropertyName),
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName areaMaskPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName hitPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_areaMaskPropertyName = areaMaskPropertyName;
			m_maxDistancePropertyName = maxDistancePropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetStructValue(m_areaMaskPropertyName, out int areaMask) &
				blackboard.TryGetStructValue(m_maxDistancePropertyName, out float maxDistance))
			{
				if (agent.SamplePathPosition(areaMask, maxDistance, out NavMeshHit hit))
				{
					blackboard.SetStructValue(m_hitPropertyName, hit);
					return Status.Success;
				}

				return Status.Failure;
			}

			return Status.Error;
		}
	}
}
