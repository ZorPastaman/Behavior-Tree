// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetRaycastHitRigidbody : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_raycastHitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName raycastHitPropertyName, BlackboardPropertyName rigidbodyPropertyName)
		{
			SetupInternal(raycastHitPropertyName, rigidbodyPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string raycastHitPropertyName, string rigidbodyPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(raycastHitPropertyName),
				new BlackboardPropertyName(rigidbodyPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName raycastHitPropertyName,
			BlackboardPropertyName rigidbodyPropertyName)
		{
			m_raycastHitPropertyName = raycastHitPropertyName;
			m_rigidbodyPropertyName = rigidbodyPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_raycastHitPropertyName, out RaycastHit raycastHit))
			{
				blackboard.SetClassValue(m_rigidbodyPropertyName, raycastHit.rigidbody);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
