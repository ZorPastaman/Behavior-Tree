// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
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
