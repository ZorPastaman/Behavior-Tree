// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetBoundsClosestPoint : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pointPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_closestPointPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName,
			BlackboardPropertyName closestPointPropertyName)
		{
			SetupInternal(boundsPropertyName, pointPropertyName, closestPointPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string boundsPropertyName, string pointPropertyName,
			string closestPointPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName), new BlackboardPropertyName(pointPropertyName),
				new BlackboardPropertyName(closestPointPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName,
			BlackboardPropertyName closestPointPropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_pointPropertyName = pointPropertyName;
			m_closestPointPropertyName = closestPointPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_pointPropertyName, out Vector3 point))
			{
				Vector3 closestPoint = bounds.ClosestPoint(point);
				blackboard.SetStructValue(m_closestPointPropertyName, closestPoint);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
