﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RigidbodyAddRelativeTorqueVariable : Action,
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
				rigidbody.AddRelativeTorque(torque, forceMode);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
