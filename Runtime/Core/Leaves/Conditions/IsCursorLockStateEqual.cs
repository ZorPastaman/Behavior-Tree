// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

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
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Lock state of type <see cref="CursorLockMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsCursorLockStateEqual : Condition, ISetupable<CursorLockMode>
	{
		[BehaviorInfo] private CursorLockMode m_lockState;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<CursorLockMode>.Setup(CursorLockMode lockState)
		{
			m_lockState = lockState;
		}

		[Pure]
		protected override Status Execute()
		{
			return StateToStatusHelper.ConditionToStatus(Cursor.lockState == m_lockState);
		}
	}
}
