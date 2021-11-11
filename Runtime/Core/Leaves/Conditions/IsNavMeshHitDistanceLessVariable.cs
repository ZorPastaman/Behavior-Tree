// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsNavMeshHitDistanceLessVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_distancePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName hitPropertyName,
			BlackboardPropertyName distancePropertyName)
		{
			SetupInternal(hitPropertyName, distancePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string hitPropertyName, string distancePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(hitPropertyName),
				new BlackboardPropertyName(distancePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName hitPropertyName, BlackboardPropertyName distancePropertyName)
		{
			m_hitPropertyName = hitPropertyName;
			m_distancePropertyName = distancePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_hitPropertyName, out NavMeshHit hit) &
				blackboard.TryGetStructValue(m_distancePropertyName, out float distance);
			return StateToStatusHelper.ConditionToStatus(hit.distance < distance, hasValues);
		}
	}
}
