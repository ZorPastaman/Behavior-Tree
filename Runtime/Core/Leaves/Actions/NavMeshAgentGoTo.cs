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
	/// Goes to a destination of type <see cref="Vector3"/> using <see cref="NavMeshAgent.SetDestination"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>when the agent's successfully reached the destination.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Running"/> </term>
	/// 		<description>if the agent is going to the destination.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the agent can't go to the destination.</description>
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
	/// 		<description>Property name of a destination of type <see cref="Vector3"/>.</description>
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
	/// <para>
	/// This <see cref="Action"/> automatically resets a path of the <see cref="NavMeshAgent"/> on its end
	/// if it still controls the movement.
	/// </para>
	/// <para>
	/// The agent's reach is determined by the expression
	/// <see cref="NavMeshAgent.remainingDistance"/> &lt;= <see cref="NavMeshAgent.stoppingDistance"/>.
	/// If it's true, the agent's reached its destination.
	/// </para>
	/// </remarks>
	public sealed class NavMeshAgentGoTo : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_destinationPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

		[BehaviorInfo] private NavMeshAgent m_agent;
		private uint m_holder;
		private bool m_dataValid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName agentPropertyName, BlackboardPropertyName destinationPropertyName,
			BlackboardPropertyName holderPropertyName)
		{
			SetupInternal(agentPropertyName, destinationPropertyName, holderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string agentPropertyName, string destinationPropertyName,
			string holderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(destinationPropertyName), new BlackboardPropertyName(holderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName destinationPropertyName, BlackboardPropertyName holderPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_destinationPropertyName = destinationPropertyName;
			m_holderPropertyName = holderPropertyName;
		}

		protected override void Begin()
		{
			base.Begin();

			m_dataValid = false;

			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetStructValue(m_destinationPropertyName, out Vector3 destination))
			{
				m_dataValid = true;

				if (agent.SetDestination(destination))
				{
					m_agent = agent;

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
				bool arrived = !m_agent.pathPending && m_agent.remainingDistance <= m_agent.stoppingDistance;
				return StateToStatusHelper.FinishedToStatus(arrived);
			}

			return StateToStatusHelper.ConditionToStatus(m_dataValid, Status.Error, Status.Failure);
		}

		protected override void End()
		{
			base.End();

			m_agent = null;
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
		}
	}
}
