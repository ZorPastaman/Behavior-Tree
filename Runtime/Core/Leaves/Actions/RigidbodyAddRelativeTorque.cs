// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Adds a relative torque to a <see cref="Rigidbody"/>.
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
	/// 		<description>Force mode of type <see cref="ForceMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class RigidbodyAddRelativeTorque : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, ForceMode>, ISetupable<string, string, ForceMode>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_torquePropertyName;
		[BehaviorInfo] private ForceMode m_forceMode;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, ForceMode>.Setup(
			BlackboardPropertyName rigidbodyPropertyName, BlackboardPropertyName torquePropertyName, ForceMode forceMode)
		{
			SetupInternal(rigidbodyPropertyName, torquePropertyName, forceMode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, ForceMode>.Setup(string rigidbodyPropertyName, string torquePropertyName,
			ForceMode forceMode)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName),
				new BlackboardPropertyName(torquePropertyName), forceMode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName,
			BlackboardPropertyName torquePropertyName, ForceMode forceMode)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_torquePropertyName = torquePropertyName;
			m_forceMode = forceMode;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null &
				blackboard.TryGetStructValue(m_torquePropertyName, out Vector3 torque))
			{
				rigidbody.AddRelativeTorque(torque, m_forceMode);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
