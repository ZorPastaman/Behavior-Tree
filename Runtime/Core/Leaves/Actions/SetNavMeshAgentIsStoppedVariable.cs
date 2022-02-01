// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="NavMeshAgent"/> stopped using <see cref="NavMeshAgent.isStopped"/>.
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
	/// 		<description>Property name of stopped of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetNavMeshAgentIsStoppedVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_isStoppedPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName isStoppedPropertyName)
		{
			SetupInternal(agentPropertyName, isStoppedPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string agentPropertyName, string isStoppedPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName),
				new BlackboardPropertyName(isStoppedPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName,
			BlackboardPropertyName isStoppedPropertyName)
		{
			m_agentPropertyName = agentPropertyName;
			m_isStoppedPropertyName = isStoppedPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null &
				blackboard.TryGetStructValue(m_isStoppedPropertyName, out bool isStopped))
			{
				agent.isStopped = isStopped;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
