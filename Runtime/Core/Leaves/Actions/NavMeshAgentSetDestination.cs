// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a destination of type <see cref="Vector3"/> using <see cref="NavMeshAgent.SetDestination"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the destination is set.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the the destination is failed to set.</description>
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
	/// This <see cref="Action"/> doesn't automatically reset a path of the <see cref="NavMeshAgent"/> on its end.
	/// It uses a movement holder only to increase it on begin
	/// so that aborted movement behaviors don't cancel its movement.
	/// </para>
	/// </remarks>
	public sealed class NavMeshAgentSetDestination : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_destinationPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_holderPropertyName;

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

			blackboard.TryGetStructValue(m_holderPropertyName, out uint holder);
			blackboard.SetStructValue(m_holderPropertyName, holder + 1);
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetStructValue(m_destinationPropertyName, out Vector3 destination))
			{
				bool set = agent.SetDestination(destination);
				return StateToStatusHelper.ConditionToStatus(set);
			}

			return Status.Error;
		}
	}
}
