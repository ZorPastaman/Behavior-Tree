// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class BoundsIntersects : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_otherBoundsPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName otherBoundsPropertyName)
		{
			SetupInternal(boundsPropertyName, otherBoundsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string boundsPropertyName, string otherBoundsPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName),
				new BlackboardPropertyName(otherBoundsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName otherBoundsPropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_otherBoundsPropertyName = otherBoundsPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
					blackboard.TryGetStructValue(m_otherBoundsPropertyName, out Bounds otherBounds);

			return StateToStatusHelper.ConditionToStatus(bounds.Intersects(otherBounds), hasValues);
		}
	}
}
