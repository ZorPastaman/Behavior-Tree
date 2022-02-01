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
	/// Checks if a current lock state is equal to a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the current state equals the specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the current state doesn't equal the specified value.</description>
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
	/// 		<description>Property name of a lock state of type <see cref="CursorLockMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsCursorLockStateEqualVariable : Condition,
		ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_lockStatePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName lockStatePropertyName)
		{
			SetupInternal(lockStatePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string lockStatePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(lockStatePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName lockStatePropertyName)
		{
			m_lockStatePropertyName = lockStatePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_lockStatePropertyName, out CursorLockMode lockState);
			return StateToStatusHelper.ConditionToStatus(Cursor.lockState == lockState, hasValue);
		}
	}
}
