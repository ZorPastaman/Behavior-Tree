// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetQuaternionZ : Action,
		ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>, ISetupable<string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;
		[BehaviorInfo] private float m_z;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName quaternionPropertyName, float z, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(quaternionPropertyName, z, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float, string>.Setup(string quaternionPropertyName, float z, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(quaternionPropertyName), z,
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName quaternionPropertyName, float z,
			BlackboardPropertyName resultPropertyName)
		{
			m_quaternionPropertyName = quaternionPropertyName;
			m_z = z;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_quaternionPropertyName, out Quaternion quaternion))
			{
				quaternion.z = m_z;
				blackboard.SetStructValue(m_resultPropertyName, quaternion);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
