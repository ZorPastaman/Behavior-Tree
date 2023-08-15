// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Casts a capsule and sets a computed <see cref="RaycastHit"/> into the <see cref="Blackboard"/>.
	/// This <see cref="Action"/> uses
	/// <see cref="Physics.CapsuleCast(Vector3, Vector3, float, Vector3, out RaycastHit, float, int)"/>.
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
	/// 		<description>Property name of a point 1 of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a point 2 of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a radius of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a direction of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Max distance of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Layer mask of type <see cref="LayerMask"/>.</description>
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
	public sealed class GetCapsuleCastHit : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			float, LayerMask, BlackboardPropertyName>,
		ISetupable<string, string, string, string, float, LayerMask, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_point1PropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_point2PropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private float m_maxDistance;
		[BehaviorInfo] private LayerMask m_layerMask;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			float, LayerMask, BlackboardPropertyName>.Setup(BlackboardPropertyName point1PropertyName,
			BlackboardPropertyName point2PropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, float maxDistance, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(point1PropertyName, point2PropertyName, radiusPropertyName, directionPropertyName,
				maxDistance, layerMask, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, float, LayerMask, string>.Setup(string point1PropertyName,
			string point2PropertyName, string radiusPropertyName, string directionPropertyName,
			float maxDistance, LayerMask layerMask, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(point1PropertyName),
				new BlackboardPropertyName(point2PropertyName), new BlackboardPropertyName(radiusPropertyName),
				new BlackboardPropertyName(directionPropertyName), maxDistance, layerMask,
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName point1PropertyName,
			BlackboardPropertyName point2PropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName directionPropertyName, float maxDistance, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			m_point1PropertyName = point1PropertyName;
			m_point2PropertyName = point2PropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_maxDistance = maxDistance;
			m_layerMask = layerMask;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_point1PropertyName, out Vector3 point1) &
				blackboard.TryGetStructValue(m_point2PropertyName, out Vector3 point2) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction);

			Status computedStatus = StateToStatusHelper.ConditionToStatus(
				Physics.CapsuleCast(point1, point2, radius, direction, out RaycastHit hit, m_maxDistance,
					m_layerMask), hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
