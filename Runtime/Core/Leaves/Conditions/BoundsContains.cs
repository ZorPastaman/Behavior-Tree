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
	/// Checks if a bounds contains a point.
	/// This <see cref="Condition"/> uses <see cref="Bounds.Contains"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the bounds contains the point.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the bounds doesn't contain the point.</description>
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
	/// </list>
	/// </para>
	/// </summary>
	public sealed class BoundsContains : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pointPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName)
		{
			SetupInternal(boundsPropertyName, pointPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string boundsPropertyName, string pointPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName),
				new BlackboardPropertyName(pointPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_pointPropertyName = pointPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_pointPropertyName, out Vector3 point);

			return StateToStatusHelper.ConditionToStatus(bounds.Contains(point), hasValues);
		}
	}
}
