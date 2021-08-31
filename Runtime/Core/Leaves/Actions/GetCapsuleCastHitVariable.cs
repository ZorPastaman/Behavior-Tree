﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetCapsuleCastHitVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_point1PropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_point2PropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maxDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName point1PropertyName, BlackboardPropertyName point2PropertyName,
			BlackboardPropertyName radiusPropertyName, BlackboardPropertyName directionPropertyName,
			BlackboardPropertyName maxDistancePropertyName, BlackboardPropertyName layerMaskPropertyName,
			BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(point1PropertyName, point2PropertyName, radiusPropertyName, directionPropertyName,
				maxDistancePropertyName, layerMaskPropertyName, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string, string, string>.Setup(string point1PropertyName,
			string point2PropertyName, string radiusPropertyName, string directionPropertyName,
			string maxDistancePropertyName, string layerMaskPropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(point1PropertyName),
				new BlackboardPropertyName(point2PropertyName), new BlackboardPropertyName(radiusPropertyName),
				new BlackboardPropertyName(directionPropertyName), new BlackboardPropertyName(maxDistancePropertyName),
				new BlackboardPropertyName(layerMaskPropertyName), new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName point1PropertyName,
			BlackboardPropertyName point2PropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName layerMaskPropertyName, BlackboardPropertyName hitPropertyName)
		{
			m_point1PropertyName = point1PropertyName;
			m_point2PropertyName = point2PropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistancePropertyName = maxDistancePropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_point1PropertyName, out Vector3 point1) &
				blackboard.TryGetStructValue(m_point2PropertyName, out Vector3 point2) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction) &
				blackboard.TryGetStructValue(m_maxDistancePropertyName, out float maxDistance) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);

			Status computedStatus = StateToStatusHelper.ConditionToStatus(
				Physics.CapsuleCast(point1, point2, radius, direction, out RaycastHit hit, maxDistance, layerMask),
				hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}