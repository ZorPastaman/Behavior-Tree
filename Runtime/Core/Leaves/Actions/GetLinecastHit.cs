// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Casts a line and sets a computed <see cref="RaycastHit"/> into the <see cref="Blackboard"/>.
	/// This <see cref="Action"/> uses
	/// <see cref="Physics.Linecast(Vector3, Vector3, out RaycastHit, int)"/>.
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
	/// 		<description>Property name of a origin of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an end of type <see cref="Vector3"/>.</description>
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
	public sealed class GetLinecastHit : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask, BlackboardPropertyName>,
		ISetupable<string, string, LayerMask, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_endPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, LayerMask, BlackboardPropertyName>.Setup(
			BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName, LayerMask layerMask,
			BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(originPropertyName, endPropertyName, layerMask, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, LayerMask, string>.Setup(string originPropertyName, string endPropertyName,
			LayerMask layerMask, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(originPropertyName), new BlackboardPropertyName(endPropertyName),
				layerMask, new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName originPropertyName, BlackboardPropertyName endPropertyName,
			LayerMask layerMask, BlackboardPropertyName hitPropertyName)
		{
			m_originPropertyName = originPropertyName;
			m_endPropertyName = endPropertyName;
			m_layerMask = layerMask;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_endPropertyName, out Vector3 end);

			Status computedStatus =
				StateToStatusHelper.ConditionToStatus(Physics.Linecast(origin, end, out RaycastHit hit, m_layerMask),
					hasValues);

			if (computedStatus == Status.Success)
			{
				blackboard.SetStructValue(m_hitPropertyName, hit);
			}

			return computedStatus;
		}
	}
}
