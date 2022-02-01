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
	/// Checks if a bounds intersects a ray.
	/// This <see cref="Condition"/> uses <see cref="Bounds.IntersectRay(Ray)"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the bounds intersects the ray.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the bounds doesn't intersect the ray.</description>
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
	/// 		<description>Property name of a ray of type <see cref="Ray"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class BoundsIntersectRay : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rayPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName rayPropertyName)
		{
			SetupInternal(boundsPropertyName, rayPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string boundsPropertyName, string rayPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName), new BlackboardPropertyName(rayPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName rayPropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_rayPropertyName = rayPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_rayPropertyName, out Ray ray);

			return StateToStatusHelper.ConditionToStatus(bounds.IntersectRay(ray), hasValues);
		}
	}
}
