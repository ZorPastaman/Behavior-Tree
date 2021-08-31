﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetRaycastHitVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maxDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>.Setup(BlackboardPropertyName originPropertyName,
			BlackboardPropertyName directionPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName layerMaskPropertyName, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(originPropertyName, directionPropertyName, maxDistancePropertyName, layerMaskPropertyName,
				hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string originPropertyName,
			string directionPropertyName, string maxDistancePropertyName, string layerMaskPropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName),
				new BlackboardPropertyName(directionPropertyName),
				new BlackboardPropertyName(maxDistancePropertyName), new BlackboardPropertyName(layerMaskPropertyName),
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName,
			BlackboardPropertyName directionPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName layerMaskPropertyName, BlackboardPropertyName hitPropertyName)
		{
			m_originPropertyName = originPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistancePropertyName = maxDistancePropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction) &
				blackboard.TryGetStructValue(m_maxDistancePropertyName, out float maxDistance) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);

			Status computedStatus = StateToStatusHelper.ConditionToStatus(
				Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, layerMask), hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
