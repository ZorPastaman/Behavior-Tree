// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class AddVector3 : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_leftOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rightOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName leftOperandPropertyName, BlackboardPropertyName rightOperandPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(leftOperandPropertyName, rightOperandPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string leftOperandPropertyName, string rightOperandPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(leftOperandPropertyName), new BlackboardPropertyName(rightOperandPropertyName),
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName leftOperandPropertyName, BlackboardPropertyName rightOperandPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_leftOperandPropertyName = leftOperandPropertyName;
			m_rightOperandPropertyName = rightOperandPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_leftOperandPropertyName, out Vector3 leftOperand) &
				blackboard.TryGetStructValue(m_rightOperandPropertyName, out Vector3 rightOperand))
			{
				blackboard.SetStructValue(m_resultPropertyName, leftOperand + rightOperand);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
