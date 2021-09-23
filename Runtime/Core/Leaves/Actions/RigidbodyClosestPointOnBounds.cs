// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class RigidbodyClosestPointOnBounds : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_closestPointPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName rigidbodyPropertyName, BlackboardPropertyName positionPropertyName,
			BlackboardPropertyName closestPointPropertyName)
		{
			SetupInternal(rigidbodyPropertyName, positionPropertyName, closestPointPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string rigidbodyPropertyName, string positionPropertyName,
			string closestPointPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName),
				new BlackboardPropertyName(positionPropertyName), new BlackboardPropertyName(closestPointPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName closestPointPropertyName)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_positionPropertyName = positionPropertyName;
			m_closestPointPropertyName = closestPointPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null &
				blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position))
			{
				blackboard.SetStructValue(m_closestPointPropertyName, rigidbody.ClosestPointOnBounds(position));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
