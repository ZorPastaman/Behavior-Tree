// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsLayerMaskValueEqual : Condition,
		ISetupable<LayerMask, BlackboardPropertyName>, ISetupable<LayerMask, string>
	{
		[BehaviorInfo] private LayerMask m_firstOperand;
		[BehaviorInfo] private BlackboardPropertyName m_secondOperandPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<LayerMask, BlackboardPropertyName>.Setup(LayerMask firstOperand,
			BlackboardPropertyName secondOperandPropertyName)
		{
			SetupInternal(firstOperand, secondOperandPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<LayerMask, string>.Setup(LayerMask firstOperand, string secondOperandPropertyName)
		{
			SetupInternal(firstOperand, new BlackboardPropertyName(secondOperandPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(LayerMask firstOperand, BlackboardPropertyName secondOperandPropertyName)
		{
			m_firstOperand = firstOperand;
			m_secondOperandPropertyName = secondOperandPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_secondOperandPropertyName, out LayerMask secondOperand);
			return StateToStatusHelper.ConditionToStatus(m_firstOperand == secondOperand, hasValue);
		}
	}
}
