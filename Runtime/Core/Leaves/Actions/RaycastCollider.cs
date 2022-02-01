// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Raycast a <see cref="Collider"/> and sets a computed <see cref="RaycastHit"/> into the <see cref="Blackboard"/>.
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
	public sealed class RaycastCollider : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colliderPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_originPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_directionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.
			Setup(BlackboardPropertyName colliderPropertyName, BlackboardPropertyName originPropertyName,
				BlackboardPropertyName directionPropertyName, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(colliderPropertyName, originPropertyName, directionPropertyName, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string colliderPropertyName, string originPropertyName,
			string directionPropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colliderPropertyName),
				new BlackboardPropertyName(originPropertyName), new BlackboardPropertyName(directionPropertyName),
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colliderPropertyName,
			BlackboardPropertyName originPropertyName, BlackboardPropertyName directionPropertyName,
			BlackboardPropertyName hitPropertyName)
		{
			m_colliderPropertyName = colliderPropertyName;
			m_originPropertyName = originPropertyName;
			m_directionPropertyName = directionPropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_colliderPropertyName, out Collider collider) & collider != null &
				blackboard.TryGetStructValue(m_originPropertyName, out Vector3 origin) &
				blackboard.TryGetStructValue(m_directionPropertyName, out Vector3 direction))
			{
				var ray = new Ray(origin, direction);
				float distance = direction.magnitude;

				if (collider.Raycast(ray, out RaycastHit hit, distance))
				{
					blackboard.SetStructValue(m_hitPropertyName, hit);
					return Status.Success;
				}

				return Status.Failure;
			}

			return Status.Error;
		}
	}
}
