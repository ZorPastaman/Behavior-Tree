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
	/// Checks if a <see cref="Quaternion.z"/> is less than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the quaternion z is less than the specified red.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the quaternion z isn't less than the specified red.</description>
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
	/// 		<description>Z of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsQuaternionZLess : Condition,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;
		[BehaviorInfo] private float m_z;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName quaternionPropertyName, float z)
		{
			SetupInternal(quaternionPropertyName, z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string quaternionPropertyName, float z)
		{
			SetupInternal(new BlackboardPropertyName(quaternionPropertyName), z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName quaternionPropertyName, float z)
		{
			m_quaternionPropertyName = quaternionPropertyName;
			m_z = z;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_quaternionPropertyName, out Quaternion quaternion);
			return StateToStatusHelper.ConditionToStatus(quaternion.z < m_z, hasValue);
		}
	}
}
