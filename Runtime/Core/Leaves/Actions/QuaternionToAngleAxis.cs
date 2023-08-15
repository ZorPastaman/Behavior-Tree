// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Computes an angle and an axis out of a <see cref="Quaternion"/>
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
	/// 		<description>Property name of a quaternion of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for an angle of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a an axis of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class QuaternionToAngleAxis : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_anglePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_axisPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName quaternionPropertyName, BlackboardPropertyName anglePropertyName,
			BlackboardPropertyName axisPropertyName)
		{
			SetupInternal(quaternionPropertyName, anglePropertyName, axisPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string quaternionPropertyName, string anglePropertyName,
			string axisPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(quaternionPropertyName),
				new BlackboardPropertyName(anglePropertyName), new BlackboardPropertyName(axisPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName quaternionPropertyName,
			BlackboardPropertyName anglePropertyName, BlackboardPropertyName axisPropertyName)
		{
			m_quaternionPropertyName = quaternionPropertyName;
			m_anglePropertyName = anglePropertyName;
			m_axisPropertyName = axisPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_quaternionPropertyName, out Quaternion quaternion))
			{
				quaternion.ToAngleAxis(out float angle, out Vector3 axis);
				blackboard.SetStructValue(m_anglePropertyName, angle);
				blackboard.SetStructValue(m_axisPropertyName, axis);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
