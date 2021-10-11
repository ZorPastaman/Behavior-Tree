// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsVector3MagnitudeGreater : Condition,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private float m_sqrMagnitude;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName vectorPropertyName, float magnitude)
		{
			SetupInternal(vectorPropertyName, magnitude);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string vectorPropertyName, float magnitude)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), magnitude);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, float magnitude)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_sqrMagnitude = magnitude * magnitude;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_vectorPropertyName, out Vector3 vector);
			return StateToStatusHelper.ConditionToStatus(vector.sqrMagnitude > m_sqrMagnitude, hasValue);
		}
	}
}
