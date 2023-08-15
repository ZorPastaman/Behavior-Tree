// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Casts a box and sets a computed <see cref="RaycastHit"/> into the <see cref="Blackboard"/>.
	/// This <see cref="Action"/> uses
	/// <see cref="Physics.BoxCast(Vector3, Vector3, Vector3, out RaycastHit, Quaternion, float, int)"/>.
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
	/// 		<description>Property name of a center of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a half extents of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a direction of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a orientation of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a max distance of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="RaycastHit"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class GetBoxCastHitVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
		BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_centerPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_halfExtentsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_orientationPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maxDistancePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName centerPropertyName, BlackboardPropertyName halfExtentsPropertyName,
			BlackboardPropertyName directionPropertyName, BlackboardPropertyName orientationPropertyName,
			BlackboardPropertyName maxDistancePropertyName, BlackboardPropertyName layerMaskPropertyName,
			BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(centerPropertyName, halfExtentsPropertyName, directionPropertyName, orientationPropertyName,
				maxDistancePropertyName, layerMaskPropertyName, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string, string, string>.Setup(string centerPropertyName,
			string halfExtentsPropertyName, string directionPropertyName, string orientationPropertyName,
			string maxDistancePropertyName, string layerMaskPropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(centerPropertyName),
				new BlackboardPropertyName(halfExtentsPropertyName), new BlackboardPropertyName(directionPropertyName),
				new BlackboardPropertyName(orientationPropertyName),
				new BlackboardPropertyName(maxDistancePropertyName), new BlackboardPropertyName(layerMaskPropertyName),
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName centerPropertyName,
			BlackboardPropertyName halfExtentsPropertyName, BlackboardPropertyName directionPropertyName,
			BlackboardPropertyName orientationPropertyName, BlackboardPropertyName maxDistancePropertyName,
			BlackboardPropertyName layerMaskPropertyName, BlackboardPropertyName hitPropertyName)
		{
			m_centerPropertyName = centerPropertyName;
			m_halfExtentsPropertyName = halfExtentsPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_orientationPropertyName = orientationPropertyName;
			m_maxDistancePropertyName = maxDistancePropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_centerPropertyName, out Vector3 center) &
				blackboard.TryGetStructValue(m_halfExtentsPropertyName, out Vector3 halfExtents) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction) &
				blackboard.TryGetStructValue(m_orientationPropertyName, out Quaternion orientation) &
				blackboard.TryGetStructValue(m_maxDistancePropertyName, out float maxDistance) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);

			Status computedStatus = StateToStatusHelper.ConditionToStatus(
				Physics.BoxCast(center, halfExtents, direction, out RaycastHit hit, orientation, maxDistance,
					layerMask), hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
