// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsRaycastHitDistanceLessVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_raycastHitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_distancePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName raycastHitPropertyName, BlackboardPropertyName distancePropertyName)
		{
			SetupInternal(raycastHitPropertyName, distancePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string raycastHitPropertyName, string distancePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(raycastHitPropertyName),
				new BlackboardPropertyName(distancePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName raycastHitPropertyName,
			BlackboardPropertyName distancePropertyName)
		{
			m_raycastHitPropertyName = raycastHitPropertyName;
			m_distancePropertyName = distancePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_raycastHitPropertyName, out RaycastHit raycastHit) &
				blackboard.TryGetStructValue(m_distancePropertyName, out float distance);
			return StateToStatusHelper.ConditionToStatus(raycastHit.distance < distance, hasValue);
		}
	}
}
