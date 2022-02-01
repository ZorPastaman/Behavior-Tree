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
	/// Checks if a box overlaps a collider.
	/// This <see cref="Condition"/> uses <see cref="Physics.CheckBox(Vector3, Vector3, Quaternion, int)"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the box overlaps a collider.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the box doesn't overlap a collider.</description>
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
	/// 		<description>Property name of a center of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a half extents of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an orientation of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class CheckBox : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, LayerMask>,
		ISetupable<string, string, string, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_centerPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_halfExtentsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_orientationPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, LayerMask>.Setup(
			BlackboardPropertyName centerPropertyName, BlackboardPropertyName halfExtentsPropertyName,
			BlackboardPropertyName orientationPropertyName, LayerMask layerMask)
		{
			SetupInternal(centerPropertyName, halfExtentsPropertyName, orientationPropertyName, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, LayerMask>.Setup(string centerPropertyName,
			string halfExtentsPropertyName, string orientationPropertyName, LayerMask layerMask)
		{
			SetupInternal(new BlackboardPropertyName(centerPropertyName),
				new BlackboardPropertyName(halfExtentsPropertyName),
				new BlackboardPropertyName(orientationPropertyName), layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName centerPropertyName,
			BlackboardPropertyName halfExtentsPropertyName, BlackboardPropertyName orientationPropertyName,
			LayerMask layerMask)
		{
			m_centerPropertyName = centerPropertyName;
			m_halfExtentsPropertyName = halfExtentsPropertyName;
			m_orientationPropertyName = orientationPropertyName;
			m_layerMask = layerMask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_centerPropertyName, out Vector3 center) &
				blackboard.TryGetStructValue(m_halfExtentsPropertyName, out Vector3 halfExtents) &
				blackboard.TryGetStructValue(m_orientationPropertyName, out Quaternion orientation);

			return StateToStatusHelper.ConditionToStatus(
				Physics.CheckBox(center, halfExtents, orientation, m_layerMask), hasValues);
		}
	}
}
