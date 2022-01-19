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
	/// Calculates a <see cref="NavMeshPath"/> using <see cref="NavMeshAgent.CalculatePath"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the path exists.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the path doesn't exist.</description>
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
	/// 		<description>Property name of a target of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a path of type <see cref="NavMeshPath"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/> or <see cref="Status.Failure"/>.
	/// But the path is empty if this <see cref="Action"/> ticks with <see cref="Status.Failure"/>.
	/// </remarks>
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
