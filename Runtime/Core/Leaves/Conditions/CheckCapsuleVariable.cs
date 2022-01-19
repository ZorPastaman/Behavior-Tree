// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class CheckCapsuleVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_startPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_endPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.
			Setup(BlackboardPropertyName startPropertyName, BlackboardPropertyName endPropertyName,
			BlackboardPropertyName radiusPropertyName, BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(startPropertyName, endPropertyName, radiusPropertyName, layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string startPropertyName,
			string endPropertyName, string radiusPropertyName, string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(startPropertyName),
				new BlackboardPropertyName(endPropertyName), new BlackboardPropertyName(radiusPropertyName),
				new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName startPropertyName, BlackboardPropertyName endPropertyName,
			BlackboardPropertyName radiusPropertyName, BlackboardPropertyName layerMaskPropertyName)
		{
			m_startPropertyName = startPropertyName;
			m_endPropertyName = endPropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_startPropertyName, out Vector3 start) &
				blackboard.TryGetStructValue(m_endPropertyName, out Vector3 end) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);

			return StateToStatusHelper.ConditionToStatus(
				Physics.CheckCapsule(start, end, radius, layerMask), hasValues);
		}
	}
}
