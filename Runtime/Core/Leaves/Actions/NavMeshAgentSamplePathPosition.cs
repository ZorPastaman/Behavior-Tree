// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class NavMeshAgentSamplePathPosition : Action,
		ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>,
		ISetupable<string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private float m_maxDistance;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName agentPropertyName, float maxDistance, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(agentPropertyName, maxDistance, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float, string>.Setup(string agentPropertyName, float maxDistance,
			string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName), maxDistance,
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName, float maxDistance,
			BlackboardPropertyName hitPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_maxDistance = maxDistance;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null)
			{
				if (agent.SamplePathPosition(NavMesh.AllAreas, m_maxDistance, out NavMeshHit hit))
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
