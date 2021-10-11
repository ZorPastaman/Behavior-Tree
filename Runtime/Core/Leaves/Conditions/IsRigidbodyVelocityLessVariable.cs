﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsRigidbodyVelocityLessVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_velocityPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName rigidbodyPropertyName, BlackboardPropertyName velocityPropertyName)
		{
			SetupInternal(rigidbodyPropertyName, velocityPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string rigidbodyPropertyName, string velocityPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName),
				new BlackboardPropertyName(velocityPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName,
			BlackboardPropertyName velocityPropertyName)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_velocityPropertyName = velocityPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null &
				blackboard.TryGetStructValue(m_velocityPropertyName, out float velocity))
			{
				return StateToStatusHelper.ConditionToStatus(rigidbody.velocity.sqrMagnitude < velocity * velocity);
			}

			return Status.Error;
		}
	}
}