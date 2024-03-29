﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a raycast hit distance is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the raycast hit distance is greater than the specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the raycast hit distance isn't greater than the specified value.</description>
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
	/// 		<description>Property name of a hit of type <see cref="RaycastHit"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a distance of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsRaycastHitDistanceGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_raycastHitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_distancePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName raycastHitPropertyName, BlackboardPropertyName distancePropertyName)
		{
			SetupInternal(raycastHitPropertyName, distancePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string raycastHitPropertyName, string distancePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(raycastHitPropertyName),
				new BlackboardPropertyName(distancePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName raycastHitPropertyName,
			BlackboardPropertyName distancePropertyName)
		{
			m_raycastHitPropertyName = raycastHitPropertyName;
			m_distancePropertyName = distancePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_raycastHitPropertyName, out RaycastHit raycastHit) &
				blackboard.TryGetStructValue(m_distancePropertyName, out float distance);
			return StateToStatusHelper.ConditionToStatus(raycastHit.distance > distance, hasValue);
		}
	}
}
