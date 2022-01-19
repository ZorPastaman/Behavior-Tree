// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class LinecastVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_endPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(originPropertyName, endPropertyName, layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string originPropertyName, string endPropertyName,
			string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName), new BlackboardPropertyName(endPropertyName),
				new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			m_originPropertyName = originPropertyName;
			m_endPropertyName = endPropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_endPropertyName, out Vector3 end) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);

			return StateToStatusHelper.ConditionToStatus(Physics.Linecast(origin, end, layerMask), hasValues);
		}
	}
}
