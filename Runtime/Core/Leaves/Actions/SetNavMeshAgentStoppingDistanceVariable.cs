// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a stopping distance to a <see cref="NavMeshAgent"/> using <see cref="NavMeshAgent.stoppingDistance"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if there's all the data in the <see cref="Blackboard"/>.</description>
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
	/// 		<description>Property name of a stopping distance of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetNavMeshAgentStoppingDistanceVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_stoppingDistancePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName stoppingDistancePropertyName)
		{
			SetupInternal(agentPropertyName, stoppingDistancePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string agentPropertyName, string stoppingDistancePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(stoppingDistancePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName stoppingDistancePropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_stoppingDistancePropertyName = stoppingDistancePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetStructValue(m_stoppingDistancePropertyName, out float stoppingDistance))
			{
				agent.stoppingDistance = stoppingDistance;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
