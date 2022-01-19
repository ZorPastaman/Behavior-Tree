// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsRigidbodyVelocityGreater : Condition,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private float m_sqrVelocity;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName rigidbodyPropertyName,
			float velocity)
		{
			SetupInternal(rigidbodyPropertyName, velocity);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string rigidbodyPropertyName, float velocity)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName), velocity);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName, float velocity)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_sqrVelocity = velocity * velocity;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null)
			{
				return StateToStatusHelper.ConditionToStatus(rigidbody.velocity.sqrMagnitude > m_sqrVelocity);
			}

			return Status.Error;
		}
	}
}
