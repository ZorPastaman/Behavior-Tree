// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="NavMeshQueryFilter"/> into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of an agent type id of type <see cref="int"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an agent mask of type <see cref="int"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="NavMeshQueryFilter"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SetNavMeshQueryFilter : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_agentTypeIdPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_areaMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_filterPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName agentTypeIdPropertyName, BlackboardPropertyName areaMaskPropertyName,
			BlackboardPropertyName filterPropertyName)
		{
			SetupInternal(agentTypeIdPropertyName, areaMaskPropertyName, filterPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string agentTypeIdPropertyName, string areaMaskPropertyName,
			string filterPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(agentTypeIdPropertyName),
				new BlackboardPropertyName(areaMaskPropertyName), new BlackboardPropertyName(filterPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName agentTypeIdPropertyName,
			BlackboardPropertyName areaMaskPropertyName, BlackboardPropertyName filterPropertyName)
		{
			m_agentTypeIdPropertyName = agentTypeIdPropertyName;
			m_areaMaskPropertyName = areaMaskPropertyName;
			m_filterPropertyName = filterPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_agentTypeIdPropertyName, out int agentTypeId) &
				blackboard.TryGetStructValue(m_areaMaskPropertyName, out int areaMask))
			{
				var filter = new NavMeshQueryFilter { agentTypeID = agentTypeId, areaMask = areaMask };
				blackboard.SetStructValue(m_filterPropertyName, filter);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
