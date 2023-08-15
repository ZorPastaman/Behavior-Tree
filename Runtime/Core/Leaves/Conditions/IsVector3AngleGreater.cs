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
	/// Checks if an angle between vectors is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the angle is greater than the specified angle.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the angle isn't greater than the specified angle.</description>
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
	/// 		<description>Property name of a first operand of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Angle of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsVector3AngleGreater : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float>, ISetupable<string, string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toPropertyName;
		[BehaviorInfo] private float m_angle;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float>.Setup(
			BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName, float angle)
		{
			SetupInternal(fromPropertyName, toPropertyName, angle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float>.Setup(string fromPropertyName, string toPropertyName, float angle)
		{
			SetupInternal(new BlackboardPropertyName(fromPropertyName), new BlackboardPropertyName(toPropertyName),
				angle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
			float angle)
		{
			m_fromPropertyName = fromPropertyName;
			m_toPropertyName = toPropertyName;
			m_angle = angle;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_fromPropertyName, out Vector3 from) &
				blackboard.TryGetStructValue(m_toPropertyName, out Vector3 to);

			return StateToStatusHelper.ConditionToStatus(Vector3.Angle(from, to) > m_angle, hasValues);
		}
	}
}
