// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class QuaternionAngle : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_anglePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstOperandPropertyName, BlackboardPropertyName secondOperandPropertyName,
			BlackboardPropertyName anglePropertyName)
		{
			SetupInternal(firstOperandPropertyName, secondOperandPropertyName, anglePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string firstOperandPropertyName, string secondOperandPropertyName,
			string anglePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstOperandPropertyName),
				new BlackboardPropertyName(secondOperandPropertyName), new BlackboardPropertyName(anglePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstOperandPropertyName,
			BlackboardPropertyName secondOperandPropertyName, BlackboardPropertyName anglePropertyName)
		{
			m_firstOperandPropertyName = firstOperandPropertyName;
			m_secondOperandPropertyName = secondOperandPropertyName;
			m_anglePropertyName = anglePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_firstOperandPropertyName, out Quaternion firstOperand) &
				blackboard.TryGetStructValue(m_secondOperandPropertyName, out Quaternion secondOperand))
			{
				blackboard.SetStructValue(m_anglePropertyName, Quaternion.Angle(firstOperand, secondOperand));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
