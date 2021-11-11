// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetNavMeshHitPosition : Action, 
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName hitPropertyName, 
			BlackboardPropertyName positionPropertyName)
		{
			SetupInternal(hitPropertyName, positionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string hitPropertyName, string positionPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(hitPropertyName),
				new BlackboardPropertyName(positionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName hitPropertyName, BlackboardPropertyName positionPropertyName)
		{
			m_hitPropertyName = hitPropertyName;
			m_positionPropertyName = positionPropertyName;
		}
		
		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_hitPropertyName, out NavMeshHit hit))
			{
				blackboard.SetStructValue(m_positionPropertyName, hit.position);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
