// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetNavMeshAgentStoppingDistance : Action,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private float m_stoppingDistance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName agentPropertyName,
			float stoppingDistance)
		{
			SetupInternal(agentPropertyName, stoppingDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string agentPropertyName, float stoppingDistance)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName), stoppingDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName, float stoppingDistance)
		{
			m_agentPropertyName = agentPropertyName;
			m_stoppingDistance = stoppingDistance;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null)
			{
				agent.stoppingDistance = m_stoppingDistance;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
