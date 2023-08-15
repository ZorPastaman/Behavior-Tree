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
	/// Checks if a distance between a bounds and a point is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the distance is greater than the specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the distance isn't greater than the specified value.</description>
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
	/// 		<description>Property name of a bounds of type <see cref="Bounds"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a point of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Value of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsBoundsDistanceGreater : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float>,
		ISetupable<string, string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pointPropertyName;
		[BehaviorInfo] private float m_sqrDistance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName, float distance)
		{
			SetupInternal(boundsPropertyName, pointPropertyName, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float>.Setup(string boundsPropertyName, string pointPropertyName,  float distance)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName), new BlackboardPropertyName(pointPropertyName),
				distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName,
			float distance)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_pointPropertyName = pointPropertyName;
			m_sqrDistance = distance * distance;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_pointPropertyName, out Vector3 point);
			bool isGreater = bounds.SqrDistance(point) > m_sqrDistance;

			return StateToStatusHelper.ConditionToStatus(isGreater, hasValues);
		}
	}
}
