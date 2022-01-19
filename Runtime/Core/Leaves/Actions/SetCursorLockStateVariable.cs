// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetCursorLockStateVariable : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_lockStatePropertyName;

		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName lockStatePropertyName)
		{
			SetupInternal(lockStatePropertyName);
		}

		void ISetupable<string>.Setup(string lockStatePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(lockStatePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName lockStatePropertyName)
		{
			m_lockStatePropertyName = lockStatePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_lockStatePropertyName, out CursorLockMode lockState))
			{
				Cursor.lockState = lockState;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
