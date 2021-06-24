// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetColliderClosestPointOnBounds : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colliderPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pointPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_closestPointPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colliderPropertyName, BlackboardPropertyName pointPropertyName,
			BlackboardPropertyName closestPointPropertyName)
		{
			SetupInternal(colliderPropertyName, pointPropertyName, closestPointPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string colliderPropertyName, string pointPropertyName,
			string closestPointPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colliderPropertyName),
				new BlackboardPropertyName(pointPropertyName), new BlackboardPropertyName(closestPointPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colliderPropertyName,
			BlackboardPropertyName pointPropertyName, BlackboardPropertyName closestPointPropertyName)
		{
			m_colliderPropertyName = colliderPropertyName;
			m_pointPropertyName = pointPropertyName;
			m_closestPointPropertyName = closestPointPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_colliderPropertyName, out Collider collider) & collider != null &
				blackboard.TryGetStructValue(m_pointPropertyName, out Vector3 point))
			{
				Vector3 closestPoint = collider.ClosestPointOnBounds(point);
				blackboard.SetStructValue(m_closestPointPropertyName, closestPoint);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
