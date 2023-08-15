// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Computes a bitwise and of layer masks and sets the result into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a first operand of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Second operand of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class LayerMaskAnd : Action,
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
				LayerMask result = firstOperand & m_secondOperand;
				blackboard.SetStructValue(m_resultPropertyName, result);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
