// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class QuaternionAngleAxis : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_anglePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_axisPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName anglePropertyName, BlackboardPropertyName axisPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			SetupInternal(anglePropertyName, axisPropertyName, quaternionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string anglePropertyName, string axisPropertyName,
			string quaternionPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(anglePropertyName), new BlackboardPropertyName(axisPropertyName),
				new BlackboardPropertyName(quaternionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName anglePropertyName, BlackboardPropertyName axisPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			m_anglePropertyName = anglePropertyName;
			m_axisPropertyName = axisPropertyName;
			m_quaternionPropertyName = quaternionPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_anglePropertyName, out float angle) &
				blackboard.TryGetStructValue(m_axisPropertyName, out Vector3 axis))
			{
				blackboard.SetStructValue(m_quaternionPropertyName, Quaternion.AngleAxis(angle, axis));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
