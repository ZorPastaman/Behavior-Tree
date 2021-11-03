// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class NavMeshAgentPatrol : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_cornersPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

		[BehaviorInfo] private NavMeshAgent m_agent;
		[BehaviorInfo] private Vector3[] m_corners;
		private uint m_holder;
		private bool m_dataValid;

		private int m_currentCornerIndex;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName agentPropertyName, BlackboardPropertyName cornersPropertyName,
			BlackboardPropertyName holderPropertyName)
		{
			SetupInternal(agentPropertyName, cornersPropertyName, holderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string agentPropertyName, string cornersPropertyName,
			string holderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(cornersPropertyName), new BlackboardPropertyName(holderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName, BlackboardPropertyName cornersPropertyName,
			BlackboardPropertyName holderPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_cornersPropertyName = cornersPropertyName;
			m_holderPropertyName = holderPropertyName;
		}

		protected override void Begin()
		{
			base.Begin();

			m_dataValid = false;

			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetClassValue(m_cornersPropertyName, out Vector3[] corners) & corners != null)
			{
				m_dataValid = true;

				m_currentCornerIndex = GetClosestCorner(agent, corners);

				if (agent.SetDestination(corners[m_currentCornerIndex]))
				{
					m_agent = agent;
					m_corners = corners;

					blackboard.TryGetStructValue(m_holderPropertyName, out uint holder);
					m_holder = holder + 1;
					blackboard.SetStructValue(m_holderPropertyName, m_holder);
				}
			}
		}

		protected override Status Execute()
		{
			if (m_agent != null)
			{
				bool arrived = !m_agent.pathPending && m_agent.remainingDistance <= m_agent.radius;

				if (arrived)
				{
					m_currentCornerIndex = GetNextCorner();
					m_agent.SetDestination(m_corners[m_currentCornerIndex]);
				}

				return Status.Running;
			}

			return StateToStatusHelper.ConditionToStatus(m_dataValid, Status.Error, Status.Failure);
		}

		protected override void End()
		{
			base.End();

			if (m_agent != null)
			{
				m_agent.ResetPath();
			}

			m_agent = null;
			m_corners = null;
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
			m_corners = null;
		}

		private static int GetClosestCorner([NotNull] NavMeshAgent agent, [NotNull] Vector3[] corners)
		{
			Vector3 agentPosition = agent.transform.position;
			float sqrLength = float.MaxValue;
			int index = 0;

			for (int i = 0, count = corners.Length; i < count; ++i)
			{
				Vector3 corner = corners[i];
				float sqrMagnitude = (corner - agentPosition).sqrMagnitude;

				if (sqrMagnitude < sqrLength)
				{
					sqrLength = sqrMagnitude;
					index = i;
				}
			}

			return index;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetNextCorner()
		{
			return (m_currentCornerIndex + 1) % m_corners.Length;
		}
	}
}
