// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class NavMeshAgentSeekVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_soughtPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_recalculateTolerancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_reachDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

		[BehaviorInfo] private NavMeshAgent m_agent;
		[BehaviorInfo] private Transform m_sought;
		private uint m_holder;
		private bool m_dataValid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>.Setup(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName soughtPropertyName, BlackboardPropertyName recalculateTolerancePropertyName,
			BlackboardPropertyName reachDistancePropertyName, BlackboardPropertyName holderPropertyName)
		{
			SetupInternal(agentPropertyName, soughtPropertyName, recalculateTolerancePropertyName,
				reachDistancePropertyName, holderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string agentPropertyName,
			string soughtPropertyName, string recalculateTolerancePropertyName, string reachDistancePropertyName,
			string holderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName), new BlackboardPropertyName(soughtPropertyName),
				new BlackboardPropertyName(recalculateTolerancePropertyName),
				new BlackboardPropertyName(reachDistancePropertyName), new BlackboardPropertyName(holderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName soughtPropertyName, BlackboardPropertyName recalculateTolerancePropertyName,
			BlackboardPropertyName reachDistancePropertyName, BlackboardPropertyName holderPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_soughtPropertyName = soughtPropertyName;
			m_recalculateTolerancePropertyName = recalculateTolerancePropertyName;
			m_reachDistancePropertyName = reachDistancePropertyName;
			m_holderPropertyName = holderPropertyName;
		}

		protected override void Begin()
		{
			base.Begin();

			m_dataValid = false;

			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetClassValue(m_soughtPropertyName, out Transform sought) & sought != null)
			{
				m_agent = agent;
				m_sought = sought;

				m_dataValid = true;

				blackboard.TryGetStructValue(m_holderPropertyName, out uint holder);
				m_holder = holder + 1;
				blackboard.SetStructValue(m_holderPropertyName, m_holder);
			}
		}

		protected override Status Execute()
		{
			if (m_agent == null || m_sought == null)
			{
				return StateToStatusHelper.ConditionToStatus(m_dataValid, Status.Error, Status.Failure);
			}

			Vector3 agentPosition = m_agent.transform.position;
			Vector3 soughtPosition = m_sought.position;
			Vector3 soughtToAgent = agentPosition - soughtPosition;

			if (!blackboard.TryGetStructValue(m_reachDistancePropertyName, out float reachDistance))
			{
				return Status.Error;
			}

			if (soughtToAgent.sqrMagnitude < reachDistance * reachDistance)
			{
				return Status.Success;
			}

			if (m_agent.pathPending || m_agent.hasPath)
			{
				if (!blackboard.TryGetStructValue(m_recalculateTolerancePropertyName, out float recalculateTolerance))
				{
					return Status.Error;
				}

				if ((m_agent.destination - soughtPosition).sqrMagnitude < recalculateTolerance * recalculateTolerance)
				{
					return Status.Running;
				}
			}

			bool found = m_agent.SetDestination(soughtPosition);

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
			m_sought = null;
		}

		protected override void OnAbort()
		{
			base.OnAbort();

			if (m_agent != null &&
				blackboard.TryGetStructValue(m_holderPropertyName, out uint holder) & m_holder == holder)
			{
				m_agent.ResetPath();
			}

			m_agent = null;
			m_sought = null;
		}
	}
}
