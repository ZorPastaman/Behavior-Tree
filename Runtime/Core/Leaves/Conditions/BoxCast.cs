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
	/// Checks if a box cast has a hit.
	/// This <see cref="Condition"/> uses
	/// <see cref="Physics.BoxCast(Vector3, Vector3, Vector3, Quaternion, float, int)"/>.
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
	/// 		<description>Property name of an orientation of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Max distance of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class BoxCast : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
		float, LayerMask>,
		ISetupable<string, string, string, string, float, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_centerPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_halfExtentsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_orientationPropertyName;
		[BehaviorInfo] private float m_maxDistance;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			float, LayerMask>.Setup(BlackboardPropertyName centerPropertyName,
			BlackboardPropertyName halfExtentsPropertyName, BlackboardPropertyName directionPropertyName,
			BlackboardPropertyName orientationPropertyName, float maxDistance, LayerMask layerMask)
		{
			SetupInternal(centerPropertyName, halfExtentsPropertyName, directionPropertyName, orientationPropertyName,
				maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, float, LayerMask>.Setup(string centerPropertyName,
			string halfExtentsPropertyName, string directionPropertyName, string orientationPropertyName,
			float maxDistance, LayerMask layerMask)
		{
			SetupInternal(new BlackboardPropertyName(centerPropertyName),
				new BlackboardPropertyName(halfExtentsPropertyName), new BlackboardPropertyName(directionPropertyName),
				new BlackboardPropertyName(orientationPropertyName), maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName centerPropertyName,
			BlackboardPropertyName halfExtentsPropertyName, BlackboardPropertyName directionPropertyName,
			BlackboardPropertyName orientationPropertyName, float maxDistance, LayerMask layerMask)
		{
			m_centerPropertyName = centerPropertyName;
			m_halfExtentsPropertyName = halfExtentsPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_orientationPropertyName = orientationPropertyName;
			m_maxDistance = maxDistance;
			m_layerMask = layerMask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_centerPropertyName, out Vector3 center) &
				blackboard.TryGetStructValue(m_halfExtentsPropertyName, out Vector3 halfExtents) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction) &
				blackboard.TryGetStructValue(m_orientationPropertyName, out Quaternion orientation);

			return StateToStatusHelper.ConditionToStatus(
				Physics.BoxCast(center, halfExtents, direction, orientation, m_maxDistance, m_layerMask), hasValues);
		}
	}
}
