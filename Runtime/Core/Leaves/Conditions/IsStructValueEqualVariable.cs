// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsStructValueEqualVariable<T> : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : struct, IEquatable<T>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstPropertyName, BlackboardPropertyName secondPropertyName)
		{
			SetupInternal(firstPropertyName, secondPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string firstPropertyName, string secondPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstPropertyName),
				new BlackboardPropertyName(secondPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstPropertyName, BlackboardPropertyName secondPropertyName)
		{
			m_firstPropertyName = firstPropertyName;
			m_secondPropertyName = secondPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_firstPropertyName, out T firstValue) &
				blackboard.TryGetStructValue(m_secondPropertyName, out T secondValue);
			return StateToStatusHelper.ConditionToStatus(firstValue.Equals(secondValue), hasValues);
		}
	}
}
