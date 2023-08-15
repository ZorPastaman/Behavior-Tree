// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a cursor lock state using <see cref="Cursor.lockState"/>.
	/// </para>
	/// <para>
	/// Always returns <see cref="Status.Success"/> in its tick.
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
	public sealed class SetCursorLockState : Action, ISetupable<CursorLockMode>
	{
		[BehaviorInfo] private CursorLockMode m_lockState;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<CursorLockMode>.Setup(CursorLockMode lockState)
		{
			m_lockState = lockState;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			Cursor.lockState = m_lockState;
			return Status.Success;
		}
	}
}
