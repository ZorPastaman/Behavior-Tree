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
	/// Checks if a sphere overlaps a collider.
	/// This <see cref="Condition"/> uses <see cref="Physics.CheckSphere(Vector3, float, int)"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the sphere overlaps a collider.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the sphere doesn't overlap a collider.</description>
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
	/// 		<description>Property name of a position of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a radius of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class CheckSphereVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_radiusPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName radiusPropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(positionPropertyName, radiusPropertyName, layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string positionPropertyName,
			string radiusPropertyName, string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(positionPropertyName),
				new BlackboardPropertyName(radiusPropertyName), new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName positionPropertyName,
			BlackboardPropertyName radiusPropertyName, BlackboardPropertyName layerMaskPropertyName)
		{
			m_positionPropertyName = positionPropertyName;
			m_radiusPropertyName = radiusPropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position) &
				blackboard.TryGetStructValue(m_radiusPropertyName, out float radius) &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask);

			return StateToStatusHelper.ConditionToStatus(Physics.CheckSphere(position, radius, layerMask), hasValues);
		}
	}
}
