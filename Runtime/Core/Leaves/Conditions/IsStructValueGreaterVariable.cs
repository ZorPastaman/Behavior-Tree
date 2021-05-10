// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

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
		public void Setup(BlackboardPropertyName leftPropertyName, BlackboardPropertyName rightPropertyName)
		{
			m_leftPropertyName = leftPropertyName;
			m_rightPropertyName = rightPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(string leftPropertyName, string rightPropertyName)
		{
			Setup(new BlackboardPropertyName(leftPropertyName), new BlackboardPropertyName(rightPropertyName));
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Failure, Status.Success};
			bool hasValues = blackboard.TryGetStructValue(m_leftPropertyName, out T leftValue) &
				blackboard.TryGetStructValue(m_rightPropertyName, out T rightValue);
			bool isGreater = leftValue.CompareTo(rightValue) > 0;
			int index = *(byte*)&hasValues << *(byte*)&isGreater;

			return results[index];
		}
	}
}
