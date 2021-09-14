// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class QuaternionLookRotation : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_forwardPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_upwardsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName forwardPropertyName, BlackboardPropertyName upwardsPropertyName, 
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(forwardPropertyName, upwardsPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string forwardPropertyName, string upwardsPropertyName, 
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(forwardPropertyName),
				new BlackboardPropertyName(upwardsPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName forwardPropertyName,
			BlackboardPropertyName upwardsPropertyName, BlackboardPropertyName resultPropertyName)
		{
			m_forwardPropertyName = forwardPropertyName;
			m_upwardsPropertyName = upwardsPropertyName;
			m_resultPropertyName = resultPropertyName;
		}
		
		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_forwardPropertyName, out Vector3 forward) &
				blackboard.TryGetStructValue(m_upwardsPropertyName, out Vector3 upwards))
			{
				blackboard.SetStructValue(m_resultPropertyName, Quaternion.LookRotation(forward, upwards));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
