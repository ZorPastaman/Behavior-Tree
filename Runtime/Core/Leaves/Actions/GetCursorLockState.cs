// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets <see cref="Cursor.lockState"/> and sets it into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// Always returns <see cref="Status.Success"/> in its tick.
	/// </para>
	/// <para>
	/// The property name for the result of type <see cref="CursorLockMode"/> is set in the setup method.
	/// </para>
	/// </summary>
	public sealed class GetCursorLockState : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_lockStatePropertyName, Cursor.lockState);
			return Status.Success;
		}
	}
}
