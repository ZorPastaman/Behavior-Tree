// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class QuaternionEuler : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_eulerPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName eulerPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			SetupInternal(eulerPropertyName, quaternionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string eulerPropertyName, string quaternionPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(eulerPropertyName),
				new BlackboardPropertyName(quaternionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName eulerPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			m_eulerPropertyName = eulerPropertyName;
			m_quaternionPropertyName = quaternionPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_eulerPropertyName, out Vector3 euler))
			{
				blackboard.SetStructValue(m_quaternionPropertyName, Quaternion.Euler(euler));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
