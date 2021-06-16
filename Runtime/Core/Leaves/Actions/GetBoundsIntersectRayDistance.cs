// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetBoundsIntersectRayDistance : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rayPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_distancePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName rayPropertyName,
			BlackboardPropertyName distancePropertyName)
		{
			SetupInternal(boundsPropertyName, rayPropertyName, distancePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string boundsPropertyName, string rayPropertyName,
			string distancePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName), new BlackboardPropertyName(rayPropertyName),
				new BlackboardPropertyName(distancePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName rayPropertyName,
			BlackboardPropertyName distancePropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_rayPropertyName = rayPropertyName;
			m_distancePropertyName = distancePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_rayPropertyName, out Ray ray))
			{
				if (bounds.IntersectRay(ray, out float distance))
				{
					blackboard.SetStructValue(m_distancePropertyName, distance);
					return Status.Success;
				}

				return Status.Failure;
			}

			return Status.Error;
		}
	}
}
