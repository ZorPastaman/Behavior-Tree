// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class LayerMaskXor : Action,
		ISetupable<BlackboardPropertyName, LayerMask, BlackboardPropertyName>, ISetupable<string, LayerMask, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstOperandPropertyName;
		[BehaviorInfo] private LayerMask m_secondOperand;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, LayerMask, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstOperandPropertyName, LayerMask secondOperand,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(firstOperandPropertyName, secondOperand, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, LayerMask, string>.Setup(string firstOperandPropertyName, LayerMask secondOperand,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstOperandPropertyName), secondOperand,
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstOperandPropertyName,
			LayerMask secondOperand, BlackboardPropertyName resultPropertyName)
		{
			m_firstOperandPropertyName = firstOperandPropertyName;
			m_secondOperand = secondOperand;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_firstOperandPropertyName, out LayerMask firstOperand))
			{
				LayerMask result = firstOperand ^ m_secondOperand;
				blackboard.SetStructValue(m_resultPropertyName, result);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
