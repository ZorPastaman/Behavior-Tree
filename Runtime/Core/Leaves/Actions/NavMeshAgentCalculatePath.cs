// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class NavMeshAgentCalculatePath : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_targetPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pathPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName agentPropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName pathPropertyName)
		{
			SetupInternal(agentPropertyName, targetPropertyName, pathPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string agentPropertyName, string targetPropertyName,
			string pathPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName), new BlackboardPropertyName(targetPropertyName),
				new BlackboardPropertyName(pathPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName pathPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_targetPropertyName = targetPropertyName;
			m_pathPropertyName = pathPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetStructValue(m_targetPropertyName, out Vector3 target))
			{
				if (!blackboard.TryGetClassValue(m_pathPropertyName, out NavMeshPath path) || path == null)
				{
					path = new NavMeshPath();
					blackboard.SetClassValue(m_pathPropertyName, path);
				}

				bool found = agent.CalculatePath(target, path);

				return StateToStatusHelper.ConditionToStatus(found);
			}

			return Status.Error;
		}
	}
}
