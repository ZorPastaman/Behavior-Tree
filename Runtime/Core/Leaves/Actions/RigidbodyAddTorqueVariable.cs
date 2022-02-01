// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Adds a torque to a <see cref="Rigidbody"/>.
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
	/// 		<description>Property name of a rigidbody of type <see cref="Rigidbody"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a torque of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a force mode of type <see cref="ForceMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class RigidbodyAddTorqueVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_torquePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_forceModePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName rigidbodyPropertyName, BlackboardPropertyName torquePropertyName,
			BlackboardPropertyName forceModePropertyName)
		{
			SetupInternal(rigidbodyPropertyName, torquePropertyName, forceModePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string rigidbodyPropertyName, string torquePropertyName,
			string forceModePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName),
				new BlackboardPropertyName(torquePropertyName), new BlackboardPropertyName(torquePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName,
			BlackboardPropertyName torquePropertyName, BlackboardPropertyName forceModePropertyName)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_torquePropertyName = torquePropertyName;
			m_forceModePropertyName = forceModePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null &
				blackboard.TryGetStructValue(m_torquePropertyName, out Vector3 torque) &
				blackboard.TryGetStructValue(m_forceModePropertyName, out ForceMode forceMode))
			{
				rigidbody.AddTorque(torque, forceMode);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
