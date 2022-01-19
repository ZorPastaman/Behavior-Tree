// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RaycastCollider : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colliderPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.
			Setup(BlackboardPropertyName colliderPropertyName, BlackboardPropertyName originPropertyName,
				BlackboardPropertyName directionPropertyName, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(colliderPropertyName, originPropertyName, directionPropertyName, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string colliderPropertyName, string originPropertyName,
			string directionPropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colliderPropertyName),
				new BlackboardPropertyName(originPropertyName), new BlackboardPropertyName(directionPropertyName),
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colliderPropertyName,
			BlackboardPropertyName originPropertyName, BlackboardPropertyName directionPropertyName,
			BlackboardPropertyName hitPropertyName)
		{
			m_colliderPropertyName = colliderPropertyName;
			m_originPropertyName = originPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_colliderPropertyName, out Collider collider) & collider != null &
				blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction))
			{
				var ray = new Ray(origin, direction);
				float distance = direction.magnitude;

				if (collider.Raycast(ray, out RaycastHit hit, distance))
				{
					blackboard.SetStructValue(m_hitPropertyName, hit);
					return Status.Success;
				}

				return Status.Failure;
			}

			return Status.Error;
		}
	}
}
