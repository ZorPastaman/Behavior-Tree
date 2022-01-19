// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Seeks a <see cref="Transform"/>. It simply always sets a destination to the target.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the agent's reached the sought.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Running"/> </term>
	/// 		<description>all the agent is seeking the sought.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the agent can't seek the sought.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Error"/> </term>
	/// 		<description>if there's no data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of an agent of type <see cref="NavMeshAgent"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a sought of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Recalculate tolerance; if the distance between the target and a current destination is more than this,
	/// 		the path is recalculated.
	/// 		</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Reach distance; the agent has reached the sought
	/// 		if the distance between the sought and the agent is less than this.
	/// 		</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Property name of a movement holder of type <see cref="uint"/>;
	/// 		it must be the same in all behaviors controlling the same <see cref="NavMeshAgent"/>.
	/// 		</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// This <see cref="Action"/> automatically resets a path of the <see cref="NavMeshAgent"/> on its end
	/// if it still controls the movement.
	/// </remarks>
	public sealed class NavMeshAgentSeek : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, float, BlackboardPropertyName>,
		ISetupable<string, string, float, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_soughtPropertyName;
		[BehaviorInfo] private float m_recalculateToleranceSqr;
		[BehaviorInfo] private float m_reachDistanceSqr;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

		[BehaviorInfo] private NavMeshAgent m_agent;
		[BehaviorInfo] private Transform m_sought;
		private uint m_holder;
		private bool m_dataValid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName agentPropertyName, BlackboardPropertyName soughtPropertyName,
			float recalculateTolerance, float reachDistance, BlackboardPropertyName holderPropertyName)
		{
			SetupInternal(agentPropertyName, soughtPropertyName, recalculateTolerance, reachDistance,
				holderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float, float, string>.Setup(string agentPropertyName, string soughtPropertyName,
			float recalculateTolerance, float reachDistance, string holderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(soughtPropertyName), recalculateTolerance, reachDistance,
				new BlackboardPropertyName(holderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName soughtPropertyName, float recalculateTolerance, float reachDistance,
			BlackboardPropertyName holderPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_soughtPropertyName = soughtPropertyName;
			m_recalculateToleranceSqr = recalculateTolerance * recalculateTolerance;
			m_reachDistanceSqr = reachDistance * reachDistance;
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

			if (soughtToAgent.sqrMagnitude < m_reachDistanceSqr)
			{
				return Status.Success;
			}

			if (m_agent.pathPending || m_agent.hasPath)
			{
				if ((m_agent.destination - soughtPosition).sqrMagnitude < m_recalculateToleranceSqr)
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
