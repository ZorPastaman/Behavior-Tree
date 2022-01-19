// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
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
