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
		[BehaviorInfo] private BlackboardPropertyName m_referenceValuePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_comparedValuePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(BlackboardPropertyName referenceValuePropertyName,
			BlackboardPropertyName comparedValuePropertyName)
		{
			m_referenceValuePropertyName = referenceValuePropertyName;
			m_comparedValuePropertyName = comparedValuePropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(string referenceValuePropertyName, string comparedValuePropertyName)
		{
			Setup(new BlackboardPropertyName(referenceValuePropertyName),
				new BlackboardPropertyName(comparedValuePropertyName));
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			if (blackboard.TryGetStructValue(m_referenceValuePropertyName, out T referenceValue) &
				blackboard.TryGetStructValue(m_comparedValuePropertyName, out T comparedValue))
			{
				Status* results = stackalloc Status[] {Status.Failure, Status.Success};
				bool isGreater = referenceValue.CompareTo(comparedValue) < 0;
				byte index = *(byte*)&isGreater;

				return results[index];
			}

			return Status.Error;
		}
	}
}
