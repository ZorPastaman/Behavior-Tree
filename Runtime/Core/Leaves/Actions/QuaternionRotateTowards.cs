// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class QuaternionRotateTowards : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>,
		ISetupable<string, string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toPropertyName;
		[BehaviorInfo] private float m_maxDegreesDelta;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName, float maxDegreesDelta,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(fromPropertyName, toPropertyName, maxDegreesDelta, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float, string>.Setup(string fromPropertyName, string toPropertyName, float maxDegreesDelta,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(fromPropertyName), new BlackboardPropertyName(toPropertyName),
				maxDegreesDelta, new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
			float maxDegreesDelta, BlackboardPropertyName resultPropertyName)
		{
			m_fromPropertyName = fromPropertyName;
			m_toPropertyName = toPropertyName;
			m_maxDegreesDelta = maxDegreesDelta;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_fromPropertyName, out Quaternion from) &
				blackboard.TryGetStructValue(m_toPropertyName, out Quaternion to))
			{
				blackboard.SetStructValue(m_resultPropertyName, Quaternion.RotateTowards(from, to, m_maxDegreesDelta));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
