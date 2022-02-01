// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a <see cref="NavMeshHit"/> distance is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>
	/// 		if the <see cref="NavMeshHit"/> distance is greater than a specified value.
	/// 		</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>
	/// 		if the <see cref="NavMeshHit"/> distance isn't greater than a specified value.
	/// 		</description>
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
	/// 		<description>Property name of an agent of type <see cref="NavMeshHit"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a distance of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsNavMeshHitDistanceGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_distancePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName hitPropertyName,
			BlackboardPropertyName distancePropertyName)
		{
			SetupInternal(hitPropertyName, distancePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string hitPropertyName, string distancePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(hitPropertyName),
				new BlackboardPropertyName(distancePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName hitPropertyName, BlackboardPropertyName distancePropertyName)
		{
			m_hitPropertyName = hitPropertyName;
			m_distancePropertyName = distancePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_hitPropertyName, out NavMeshHit hit) &
				blackboard.TryGetStructValue(m_distancePropertyName, out float distance);
			return StateToStatusHelper.ConditionToStatus(hit.distance > distance, hasValues);
		}
	}
}
