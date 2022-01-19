// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetQuaternionVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_xPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_yPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_zPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_wPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>.Setup(BlackboardPropertyName xPropertyName, BlackboardPropertyName yPropertyName,
			BlackboardPropertyName zPropertyName, BlackboardPropertyName wPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			SetupInternal(xPropertyName, yPropertyName, zPropertyName, wPropertyName, quaternionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string xPropertyName, string yPropertyName,
			string zPropertyName, string wPropertyName, string quaternionPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(xPropertyName), new BlackboardPropertyName(yPropertyName),
				new BlackboardPropertyName(zPropertyName), new BlackboardPropertyName(wPropertyName),
				new BlackboardPropertyName(quaternionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName xPropertyName, BlackboardPropertyName yPropertyName,
			BlackboardPropertyName zPropertyName, BlackboardPropertyName wPropertyName,
			BlackboardPropertyName quaternionPropertyName)
		{
			m_xPropertyName = xPropertyName;
			m_yPropertyName = yPropertyName;
			m_zPropertyName = zPropertyName;
			m_wPropertyName = wPropertyName;
			m_quaternionPropertyName = quaternionPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_xPropertyName, out float x) &
				blackboard.TryGetStructValue(m_yPropertyName, out float y) &
				blackboard.TryGetStructValue(m_zPropertyName, out float z) &
				blackboard.TryGetStructValue(m_wPropertyName, out float w))
			{
				blackboard.SetStructValue(m_quaternionPropertyName, new Quaternion(x, y, z, w));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
