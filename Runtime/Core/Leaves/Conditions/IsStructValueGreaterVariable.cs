﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsStructValueGreaterVariable<T> : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : struct, IComparable<T>
	{
		[BehaviorInfo] private BlackboardPropertyName m_leftPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rightPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName leftPropertyName, BlackboardPropertyName rightPropertyName)
		{
			SetupInternal(leftPropertyName, rightPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string leftPropertyName, string rightPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(leftPropertyName), new BlackboardPropertyName(rightPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName leftPropertyName, BlackboardPropertyName rightPropertyName)
		{
			m_leftPropertyName = leftPropertyName;
			m_rightPropertyName = rightPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_leftPropertyName, out T leftValue) &
				blackboard.TryGetStructValue(m_rightPropertyName, out T rightValue);
			return StateToStatusHelper.ConditionToStatus(leftValue.CompareTo(rightValue) > 0, hasValues);
		}
	}
}
