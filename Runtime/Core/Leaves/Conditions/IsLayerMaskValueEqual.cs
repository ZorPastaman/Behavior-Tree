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
	/// 		<description>Layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
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
