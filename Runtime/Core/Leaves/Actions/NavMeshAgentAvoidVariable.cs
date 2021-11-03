// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class NavMeshAgentAvoidVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_avoidedPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_minDistanceToAvoidedPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_minDotToAvoidedPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_avoidDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

		[BehaviorInfo] private NavMeshAgent m_agent;
		[BehaviorInfo] private Transform m_avoided;
		private uint m_holder;
		private bool m_dataValid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
				BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName agentPropertyName, BlackboardPropertyName avoidedPropertyName,
			BlackboardPropertyName minDistanceToAvoidedPropertyName, BlackboardPropertyName minDotToAvoidedPropertyName,
			BlackboardPropertyName avoidDistancePropertyName, BlackboardPropertyName holderPropertyName)
		{
			SetupInternal(agentPropertyName, avoidedPropertyName, minDistanceToAvoidedPropertyName,
				minDotToAvoidedPropertyName, avoidDistancePropertyName, holderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string, string>.Setup(string agentPropertyName,
			string avoidedPropertyName, string minDistanceToAvoidedPropertyName, string minDotToAvoidedPropertyName,
			string avoidDistancePropertyName, string holderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(avoidedPropertyName),
				new BlackboardPropertyName(minDistanceToAvoidedPropertyName),
				new BlackboardPropertyName(minDotToAvoidedPropertyName),
				new BlackboardPropertyName(avoidDistancePropertyName), new BlackboardPropertyName(holderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName, BlackboardPropertyName avoidedPropertyName,
			BlackboardPropertyName minDistanceToAvoidedPropertyName, BlackboardPropertyName minDotToAvoidedPropertyName,
			BlackboardPropertyName avoidDistancePropertyName, BlackboardPropertyName holderPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_avoidedPropertyName = avoidedPropertyName;
			m_minDistanceToAvoidedPropertyName = minDistanceToAvoidedPropertyName;
			m_avoidDistancePropertyName = avoidDistancePropertyName;
			m_minDotToAvoidedPropertyName = minDotToAvoidedPropertyName;
			m_holderPropertyName = holderPropertyName;
		}

		protected override void Begin()
		{
			base.Begin();

			m_dataValid = false;

			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetClassValue(m_avoidedPropertyName, out Transform avoided) & avoided != null)
			{
				m_agent = agent;
				m_avoided = avoided;

				m_dataValid = true;

				blackboard.TryGetStructValue(m_holderPropertyName, out uint holder);
				m_holder = holder + 1;
				blackboard.SetStructValue(m_holderPropertyName, m_holder);
			}
		}

		protected override Status Execute()
		{
			if (m_agent == null || m_avoided == null)
			{
				return StateToStatusHelper.ConditionToStatus(m_dataValid, Status.Error, Status.Failure);
			}

			Vector3 agentPosition = m_agent.transform.position;
			Vector3 avoidedPosition = m_avoided.position;
			Vector3 avoidedToAgent = agentPosition - avoidedPosition;

			if (!blackboard.TryGetStructValue(m_minDistanceToAvoidedPropertyName, out float minDistanceToAvoidedSqr))
			{
				return Status.Error;
			}

			minDistanceToAvoidedSqr *= minDistanceToAvoidedSqr;

			if (avoidedToAgent.sqrMagnitude > minDistanceToAvoidedSqr)
			{
				return Status.Success;
			}

			if (m_agent.pathPending || m_agent.hasPath)
			{
				Vector3 destination = m_agent.destination;
				Vector3 avoidedToDestination = destination - avoidedPosition;

				if (!blackboard.TryGetStructValue(m_minDotToAvoidedPropertyName, out float minDotToAvoided))
				{
					return Status.Error;
				}

				if (avoidedToDestination.sqrMagnitude > minDistanceToAvoidedSqr &&
					Vector3.Dot((destination - agentPosition).normalized, avoidedToDestination.normalized) >
					minDotToAvoided)
				{
					return Status.Running;
				}
			}

			if (!blackboard.TryGetStructValue(m_avoidDistancePropertyName, out float avoidDistance))
			{
				return Status.Error;
			}

			Vector3 hidePosition = agentPosition + avoidedToAgent.normalized * avoidDistance;
			bool found = NavMesh.SamplePosition(hidePosition, out NavMeshHit hit, avoidDistance, m_agent.areaMask) &&
				(hit.position - avoidedPosition).sqrMagnitude > minDistanceToAvoidedSqr &&
				m_agent.SetDestination(hit.position);

			return StateToStatusHelper.FailedToStatus(!found);
		}

		protected override void End()
		{
			base.End();

			if (m_agent != null)
			{
				m_agent.ResetPath();
			}

			m_agent = null;
			m_avoided = null;
		}

		protected override void OnAbort()
		{
			base.OnAbort();

			if (m_agent != null
				&& blackboard.TryGetStructValue(m_holderPropertyName, out uint holder) & m_holder == holder)
			{
				m_agent.ResetPath();
			}

			m_agent = null;
			m_avoided = null;
		}
	}
}
