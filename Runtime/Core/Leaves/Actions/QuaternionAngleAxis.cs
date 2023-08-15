// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Computes a <see cref="Quaternion"/> with an angle and an axis
	/// and sets a result into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if there's all the data in the <see cref="Blackboard"/>.</description>
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
	/// 		<description>Property name of an angle of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an axis of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class QuaternionAngleAxis : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_anglePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_axisPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName anglePropertyName, BlackboardPropertyName axisPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			SetupInternal(anglePropertyName, axisPropertyName, quaternionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string anglePropertyName, string axisPropertyName,
			string quaternionPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(anglePropertyName), new BlackboardPropertyName(axisPropertyName),
				new BlackboardPropertyName(quaternionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName anglePropertyName, BlackboardPropertyName axisPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			m_anglePropertyName = anglePropertyName;
			m_axisPropertyName = axisPropertyName;
			m_quaternionPropertyName = quaternionPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_anglePropertyName, out float angle) &
				blackboard.TryGetStructValue(m_axisPropertyName, out Vector3 axis))
			{
				blackboard.SetStructValue(m_quaternionPropertyName, Quaternion.AngleAxis(angle, axis));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
