// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
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
