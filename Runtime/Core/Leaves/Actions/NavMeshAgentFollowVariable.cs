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
	/// Follows a <see cref="Transform"/>. It simply always sets a destination to the target.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Running"/> </term>
	/// 		<description>all the time when the data is correct and a path to the target exists.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the agent can't follow the target.</description>
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
	/// 		<description>Property name of a target of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Property name of recalculate tolerance; if the distance between the target
	/// 		and a current destination is more than this, the path is recalculated.
	/// 		</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Property name of reach distance; if the distance between the target and the agent is less than this,
	/// 		the agent is stopped.
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
	public sealed class NavMeshAgentFollowVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_followedPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_recalculateTolerancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_reachDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

		[BehaviorInfo] private NavMeshAgent m_agent;
		[BehaviorInfo] private Transform m_followed;
		private uint m_holder;
		private bool m_dataValid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>.Setup(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName followedPropertyName, BlackboardPropertyName recalculateTolerancePropertyName,
			BlackboardPropertyName reachDistancePropertyName, BlackboardPropertyName holderPropertyName)
		{
			SetupInternal(agentPropertyName, followedPropertyName, recalculateTolerancePropertyName,
				reachDistancePropertyName, holderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string agentPropertyName,
			string followedPropertyName, string recalculateTolerancePropertyName, string reachDistancePropertyName,
			string holderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(followedPropertyName),
				new BlackboardPropertyName(recalculateTolerancePropertyName),
				new BlackboardPropertyName(reachDistancePropertyName), new BlackboardPropertyName(holderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName followedPropertyName, BlackboardPropertyName recalculateTolerancePropertyName,
			BlackboardPropertyName reachDistancePropertyName, BlackboardPropertyName holderPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_followedPropertyName = followedPropertyName;
			m_recalculateTolerancePropertyName = recalculateTolerancePropertyName;
			m_reachDistancePropertyName = reachDistancePropertyName;
			m_holderPropertyName = holderPropertyName;
		}

		protected override void Begin()
		{
			base.Begin();

			m_dataValid = false;

			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetClassValue(m_followedPropertyName, out Transform followed) & followed != null)
			{
				m_agent = agent;
				m_followed = followed;

				m_dataValid = true;

				blackboard.TryGetStructValue(m_holderPropertyName, out uint holder);
				m_holder = holder + 1;
				blackboard.SetStructValue(m_holderPropertyName, m_holder);
			}
		}

		protected override Status Execute()
		{
			if (m_agent == null || m_followed == null)
			{
				return StateToStatusHelper.ConditionToStatus(m_dataValid, Status.Error, Status.Failure);
			}

			Vector3 agentPosition = m_agent.transform.position;
			Vector3 followedPosition = m_followed.position;
			Vector3 followedToAgent = agentPosition - followedPosition;

			if (!blackboard.TryGetStructValue(m_reachDistancePropertyName, out float reachDistance))
			{
				return Status.Error;
			}

			if (followedToAgent.sqrMagnitude < reachDistance * reachDistance)
			{
				m_agent.ResetPath();
				return Status.Running;
			}

			if (m_agent.pathPending || m_agent.hasPath)
			{
				if (!blackboard.TryGetStructValue(m_recalculateTolerancePropertyName, out float recalculateTolerance))
				{
					return Status.Error;
				}

				if ((m_agent.destination - followedPosition).sqrMagnitude < recalculateTolerance * recalculateTolerance)
				{
					return Status.Running;
				}
			}

			bool found = m_agent.SetDestination(followedPosition);

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
			m_followed = null;
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
			m_followed = null;
		}
	}
}
