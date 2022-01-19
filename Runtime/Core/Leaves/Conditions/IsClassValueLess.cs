// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsClassValueLess<T> : Condition, ISetupable<T, BlackboardPropertyName>, ISetupable<T, string>
		where T : class, IComparable<T>
	{
		[BehaviorInfo] private T m_value;
		[BehaviorInfo] private BlackboardPropertyName m_propertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<T, BlackboardPropertyName>.Setup(T value, BlackboardPropertyName propertyName)
		{
			SetupInternal(value, propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<T, string>.Setup(T value, string propertyName)
		{
			SetupInternal(value, new BlackboardPropertyName(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetClassValue(m_propertyName, out T value);
			bool isLess = m_value != null && m_value.CompareTo(value) > 0;
			return StateToStatusHelper.ConditionToStatus(isLess, hasValue);
		}
	}
}
