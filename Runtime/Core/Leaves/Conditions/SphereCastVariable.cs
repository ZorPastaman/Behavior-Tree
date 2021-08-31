﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class SphereCastVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maxDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(originPropertyName, radiusPropertyName, directionPropertyName, maxDistancePropertyName,
				layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string originPropertyName,
			string radiusPropertyName, string directionPropertyName, string maxDistancePropertyName,
			string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName),
				new BlackboardPropertyName(radiusPropertyName), new BlackboardPropertyName(directionPropertyName),
				new BlackboardPropertyName(maxDistancePropertyName), new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			m_originPropertyName = originPropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistancePropertyName = maxDistancePropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction) &
				blackboard.TryGetStructValue(m_maxDistancePropertyName, out float maxDistance) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);
			var ray = new Ray(origin, direction);

			return StateToStatusHelper.ConditionToStatus(Physics.SphereCast(ray, radius, maxDistance, layerMask),
				hasValues);
		}
	}
}
