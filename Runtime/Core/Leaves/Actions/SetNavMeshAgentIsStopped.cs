// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

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
	/// 		<description>Stopped of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetNavMeshAgentIsStopped : Action,
		ISetupable<BlackboardPropertyName, bool>, ISetupable<string, bool>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentPropertyName;
		[BehaviorInfo] private bool m_isStopped;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool>.Setup(BlackboardPropertyName agentPropertyName, bool isStopped)
		{
			SetupInternal(agentPropertyName, isStopped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool>.Setup(string agentPropertyName, bool isStopped)
		{
			SetupInternal(new BlackboardPropertyName(agentPropertyName), isStopped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentPropertyName, bool isStopped)
		{
			m_agentPropertyName = agentPropertyName;
			m_isStopped = isStopped;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_agentPropertyName, out NavMeshAgent agent) & agent != null)
			{
				agent.isStopped = m_isStopped;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
