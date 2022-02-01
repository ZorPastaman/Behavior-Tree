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
	/// Checks if an angle between quaternions is greater than a specified value.
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
	/// 		<description>Property name of a first operand of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Angle of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsQuaternionAngleGreater : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float>,
		ISetupable<string, string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondOperandPropertyName;
		[BehaviorInfo] private float m_angle;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float>.Setup(
			BlackboardPropertyName firstOperandPropertyName, BlackboardPropertyName secondOperandPropertyName,
			float angle)
		{
			SetupInternal(firstOperandPropertyName, secondOperandPropertyName, angle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float>.Setup(string firstOperandPropertyName, string secondOperandPropertyName,
			float angle)
		{
			SetupInternal(new BlackboardPropertyName(firstOperandPropertyName),
				new BlackboardPropertyName(secondOperandPropertyName), angle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstOperandPropertyName,
			BlackboardPropertyName secondOperandPropertyName, float angle)
		{
			m_firstOperandPropertyName = firstOperandPropertyName;
			m_secondOperandPropertyName = secondOperandPropertyName;
			m_angle = angle;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_firstOperandPropertyName, out Quaternion first) &
				blackboard.TryGetStructValue(m_secondOperandPropertyName, out Quaternion second);

			return StateToStatusHelper.ConditionToStatus(Quaternion.Angle(first, second) > m_angle, hasValues);
		}
	}
}
