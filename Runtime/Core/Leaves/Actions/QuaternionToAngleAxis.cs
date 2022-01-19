// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class QuaternionToAngleAxis : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_anglePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_axisPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName quaternionPropertyName, BlackboardPropertyName anglePropertyName,
			BlackboardPropertyName axisPropertyName)
		{
			SetupInternal(quaternionPropertyName, anglePropertyName, axisPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string quaternionPropertyName, string anglePropertyName,
			string axisPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(quaternionPropertyName),
				new BlackboardPropertyName(anglePropertyName), new BlackboardPropertyName(axisPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName quaternionPropertyName,
			BlackboardPropertyName anglePropertyName, BlackboardPropertyName axisPropertyName)
		{
			m_quaternionPropertyName = quaternionPropertyName;
			m_anglePropertyName = anglePropertyName;
			m_axisPropertyName = axisPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_quaternionPropertyName, out Quaternion quaternion))
			{
				quaternion.ToAngleAxis(out float angle, out Vector3 axis);
				blackboard.SetStructValue(m_anglePropertyName, angle);
				blackboard.SetStructValue(m_axisPropertyName, axis);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
