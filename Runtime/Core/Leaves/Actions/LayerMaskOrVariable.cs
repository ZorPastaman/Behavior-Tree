// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class LayerMaskOrVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstOperandPropertyName, BlackboardPropertyName secondOperandPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(firstOperandPropertyName, secondOperandPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string firstOperandPropertyName, string secondOperandPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstOperandPropertyName),
				new BlackboardPropertyName(secondOperandPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstOperandPropertyName,
			BlackboardPropertyName secondOperandPropertyName, BlackboardPropertyName resultPropertyName)
		{
			m_firstOperandPropertyName = firstOperandPropertyName;
			m_secondOperandPropertyName = secondOperandPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_firstOperandPropertyName, out LayerMask firstOperand) &
				blackboard.TryGetStructValue(m_secondOperandPropertyName, out LayerMask secondOperand))
			{
				LayerMask result = firstOperand | secondOperand;
				blackboard.SetStructValue(m_resultPropertyName, result);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
