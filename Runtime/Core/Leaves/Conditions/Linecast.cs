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
	/// Checks if a line cast has a hit.
	/// This <see cref="Condition"/> uses
	/// <see cref="Physics.Linecast(Vector3, Vector3, int)"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the hit exists.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the hit doesn't exist.</description>
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
	/// 		<description>Property name of an origin of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an end of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class Linecast : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask>,
		ISetupable<string, string, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_endPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName, LayerMask layerMask)
		{
			SetupInternal(originPropertyName, endPropertyName, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, LayerMask>.Setup(string originPropertyName, string endPropertyName,
			LayerMask layerMask)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName), new BlackboardPropertyName(endPropertyName),
				layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName,
			LayerMask layerMask)
		{
			m_originPropertyName = originPropertyName;
			m_endPropertyName = endPropertyName;
			m_layerMask = layerMask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_endPropertyName, out Vector3 end);

			return StateToStatusHelper.ConditionToStatus(Physics.Linecast(origin, end, m_layerMask), hasValues);
		}
	}
}
