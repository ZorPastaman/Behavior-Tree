// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetNavMeshHitDistance : Action,
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

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_hitPropertyName, out NavMeshHit hit))
			{
				blackboard.SetStructValue(m_distancePropertyName, hit.distance);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
