// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class CheckCapsule : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, LayerMask>,
		ISetupable<string, string, string, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_startPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_endPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, LayerMask>.
			Setup(BlackboardPropertyName startPropertyName, BlackboardPropertyName endPropertyName, 
			BlackboardPropertyName radiusPropertyName, LayerMask layerMask)
		{
			SetupInternal(startPropertyName, endPropertyName, radiusPropertyName, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, LayerMask>.Setup(string startPropertyName,
			string endPropertyName, string radiusPropertyName, LayerMask layerMask)
		{
			SetupInternal(new BlackboardPropertyName(startPropertyName),
				new BlackboardPropertyName(endPropertyName), new BlackboardPropertyName(radiusPropertyName),
				layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName startPropertyName,
			BlackboardPropertyName endPropertyName, BlackboardPropertyName radiusPropertyName, LayerMask layerMask)
		{
			m_startPropertyName = startPropertyName;
			m_endPropertyName = endPropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_layerMask = layerMask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_startPropertyName, out Vector3 start) &
				blackboard.TryGetStructValue(m_endPropertyName, out Vector3 end) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius);

			return StateToStatusHelper.ConditionToStatus(
				Physics.CheckCapsule(start, end, radius, m_layerMask), hasValues);
		}
	}
}
