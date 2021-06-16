// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class BoundsContains : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pointPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName)
		{
			SetupInternal(boundsPropertyName, pointPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string boundsPropertyName, string pointPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName),
				new BlackboardPropertyName(pointPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_pointPropertyName = pointPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_pointPropertyName, out Vector3 point);

			return StateToStatusHelper.ConditionToStatus(bounds.Contains(point), hasValues);
		}
	}
}
