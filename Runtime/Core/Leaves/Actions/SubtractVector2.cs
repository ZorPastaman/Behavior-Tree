// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Subtracts vectors. This takes operands from its <see cref="Blackboard"/>
	/// and sets a result into its <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if there's all the data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Error"/> </term>
	/// 		<description>if there's no data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of a first operand of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SubtractVector2 : Action,
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
			if (blackboard.TryGetStructValue(m_leftOperandPropertyName, out Vector2 leftOperand) &
				blackboard.TryGetStructValue(m_rightOperandPropertyName, out Vector2 rightOperand))
			{
				blackboard.SetStructValue(m_resultPropertyName, leftOperand - rightOperand);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
