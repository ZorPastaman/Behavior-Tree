// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class CheckSphere : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask>,
		ISetupable<string, string, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask>.Setup(
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName radiusPropertyName,
			LayerMask layerMask)
		{
			SetupInternal(positionPropertyName, radiusPropertyName, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, LayerMask>.Setup(string positionPropertyName,
			string radiusPropertyName, LayerMask layerMask)
		{
			SetupInternal(new BlackboardPropertyName(positionPropertyName),
				new BlackboardPropertyName(radiusPropertyName), layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName positionPropertyName,
			BlackboardPropertyName radiusPropertyName, LayerMask layerMask)
		{
			m_positionPropertyName = positionPropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_layerMask = layerMask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius);

			return StateToStatusHelper.ConditionToStatus(Physics.CheckSphere(position, radius, m_layerMask),
				hasValues);
		}
	}
}
