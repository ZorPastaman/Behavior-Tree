// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetCapsuleCastHit : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			float, LayerMask, BlackboardPropertyName>,
		ISetupable<string, string, string, string, float, LayerMask, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_point1PropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_point2PropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private float m_maxDistance;
		[BehaviorInfo] private LayerMask m_layerMask;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			float, LayerMask, BlackboardPropertyName>.Setup(BlackboardPropertyName point1PropertyName,
			BlackboardPropertyName point2PropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, float maxDistance, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(point1PropertyName, point2PropertyName, radiusPropertyName, directionPropertyName,
				maxDistance, layerMask, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, float, LayerMask, string>.Setup(string point1PropertyName,
			string point2PropertyName, string radiusPropertyName, string directionPropertyName,
			float maxDistance, LayerMask layerMask, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(point1PropertyName),
				new BlackboardPropertyName(point2PropertyName), new BlackboardPropertyName(radiusPropertyName),
				new BlackboardPropertyName(directionPropertyName), maxDistance, layerMask,
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName point1PropertyName,
			BlackboardPropertyName point2PropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, float maxDistance, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			m_point1PropertyName = point1PropertyName;
			m_point2PropertyName = point2PropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistance = maxDistance;
			m_layerMask = layerMask;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_point1PropertyName, out Vector3 point1) &
				blackboard.TryGetStructValue(m_point2PropertyName, out Vector3 point2) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction);

			Status computedStatus = StateToStatusHelper.ConditionToStatus(
				Physics.CapsuleCast(point1, point2, radius, direction, out RaycastHit hit, m_maxDistance,
					m_layerMask), hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
