// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class Raycast : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, LayerMask>,
		ISetupable<string, string, float, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private float m_maxDistance;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, LayerMask>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName directionPropertyName, float maxDistance,
			LayerMask layerMask)
		{
			SetupInternal(originPropertyName, directionPropertyName, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float, LayerMask>.Setup(string originPropertyName, string directionPropertyName,
			float maxDistance, LayerMask layerMask)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName),
				new BlackboardPropertyName(directionPropertyName), maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName,
			BlackboardPropertyName directionPropertyName, float maxDistance, LayerMask layerMask)
		{
			m_originPropertyName = originPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistance = maxDistance;
			m_layerMask = layerMask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction);

			return StateToStatusHelper.ConditionToStatus(Physics.Raycast(origin, direction, m_maxDistance, m_layerMask),
				hasValues);
		}
	}
}
