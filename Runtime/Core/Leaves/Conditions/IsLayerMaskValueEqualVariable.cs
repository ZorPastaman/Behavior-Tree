// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a layer mask property is equal to a specified layer mask.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the layer mask property is equal to a specified layer mask.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the layer mask property isn't equal to a specified layer mask.</description>
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
	/// 		<description>Property name of a layer mask property of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsLayerMaskValueEqualVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondOperandPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstOperandPropertyName, BlackboardPropertyName secondOperandPropertyName)
		{
			SetupInternal(firstOperandPropertyName, secondOperandPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string firstOperandPropertyName, string secondOperandPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstOperandPropertyName),
				new BlackboardPropertyName(secondOperandPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstOperandPropertyName,
			BlackboardPropertyName secondOperandPropertyName)
		{
			m_firstOperandPropertyName = firstOperandPropertyName;
			m_secondOperandPropertyName = secondOperandPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_firstOperandPropertyName, out LayerMask firstOperand) &
				blackboard.TryGetStructValue(m_secondOperandPropertyName, out LayerMask secondOperand);
			return StateToStatusHelper.ConditionToStatus(firstOperand == secondOperand, hasValues);
		}
	}
}
