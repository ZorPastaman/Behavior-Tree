// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

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
		public void Setup([CanBeNull] T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup([CanBeNull] T value, string propertyName)
		{
			Setup(value, new BlackboardPropertyName(propertyName));
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Failure, Status.Success};
			bool hasValue = blackboard.TryGetClassValue(m_propertyName, out T value);
			bool isLess = m_value != null && m_value.CompareTo(value) > 0;
			int index = *(byte*)&hasValue << *(byte*)&isLess;

			return results[index];
		}
	}
}
