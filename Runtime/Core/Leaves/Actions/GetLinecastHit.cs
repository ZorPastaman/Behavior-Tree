// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetLinecastHit : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask, BlackboardPropertyName>,
		ISetupable<string, string, LayerMask, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_endPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask, BlackboardPropertyName>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(originPropertyName, endPropertyName, layerMask, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, LayerMask, string>.Setup(string originPropertyName, string endPropertyName,
			LayerMask layerMask, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName), new BlackboardPropertyName(endPropertyName),
				layerMask, new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName,
			LayerMask layerMask, BlackboardPropertyName hitPropertyName)
		{
			m_originPropertyName = originPropertyName;
			m_endPropertyName = endPropertyName;
			m_layerMask = layerMask;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_endPropertyName, out Vector3 end);

			Status computedStatus =
				StateToStatusHelper.ConditionToStatus(Physics.Linecast(origin, end, out RaycastHit hit, m_layerMask),
					hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
