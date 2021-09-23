// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RigidbodyAddRelativeForce : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, ForceMode>, ISetupable<string, string, ForceMode>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_forcePropertyName;
		[BehaviorInfo] private ForceMode m_forceMode;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, ForceMode>.Setup(
			BlackboardPropertyName rigidbodyPropertyName, BlackboardPropertyName forcePropertyName, ForceMode forceMode)
		{
			SetupInternal(rigidbodyPropertyName, forcePropertyName, forceMode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, ForceMode>.Setup(string rigidbodyPropertyName, string forcePropertyName,
			ForceMode forceMode)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName),
				new BlackboardPropertyName(forcePropertyName), forceMode);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName,
			BlackboardPropertyName forcePropertyName, ForceMode forceMode)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_forcePropertyName = forcePropertyName;
			m_forceMode = forceMode;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null &
				blackboard.TryGetStructValue(m_forcePropertyName, out Vector3 force))
			{
				rigidbody.AddRelativeForce(force, m_forceMode);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
