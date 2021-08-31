// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetRaycastHit : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, LayerMask, BlackboardPropertyName>,
		ISetupable<string, string, float, LayerMask, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private float m_maxDistance;
		[BehaviorInfo] private LayerMask m_layerMask;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, LayerMask, BlackboardPropertyName>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName directionPropertyName,
			float maxDistance, LayerMask layerMask, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(originPropertyName, directionPropertyName, maxDistance, layerMask, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float, LayerMask, string>.Setup(string originPropertyName,
			string directionPropertyName, float maxDistance, LayerMask layerMask, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName),
				new BlackboardPropertyName(directionPropertyName),
				maxDistance, layerMask, new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName,
			BlackboardPropertyName directionPropertyName, float maxDistance, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			m_originPropertyName = originPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistance = maxDistance;
			m_layerMask = layerMask;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction);

			Status computedStatus = StateToStatusHelper.ConditionToStatus(
				Physics.Raycast(origin, direction, out RaycastHit hit, m_maxDistance, m_layerMask), hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
