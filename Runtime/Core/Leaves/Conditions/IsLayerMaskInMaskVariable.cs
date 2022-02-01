// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a layer mask property is in a specified layer mask.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the layer mask property is in a specified layer mask.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the layer mask property isn't in a specified layer mask.</description>
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
	public sealed class IsLayerMaskInMaskVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName valuePropertyName,
			BlackboardPropertyName maskPropertyName)
		{
			SetupInternal(valuePropertyName, maskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string valuePropertyName, string maskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(valuePropertyName), new BlackboardPropertyName(maskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName valuePropertyName, BlackboardPropertyName maskPropertyName)
		{
			m_valuePropertyName = valuePropertyName;
			m_maskPropertyName = maskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_valuePropertyName, out LayerMask value) &
				blackboard.TryGetStructValue(m_maskPropertyName, out LayerMask mask);
			return StateToStatusHelper.ConditionToStatus((value & mask) == value, hasValues);
		}
	}
}
