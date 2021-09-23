// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RigidbodyAddTorque : Action,
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
				rigidbody.AddTorque(torque, m_forceMode);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
