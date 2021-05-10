// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsClassValueEqualVariable<T> : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
		where T : class
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(BlackboardPropertyName firstPropertyName, BlackboardPropertyName secondPropertyName)
		{
			m_firstPropertyName = firstPropertyName;
			m_secondPropertyName = secondPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Setup(string firstPropertyName, string secondPropertyName)
		{
			Setup(new BlackboardPropertyName(firstPropertyName), new BlackboardPropertyName(secondPropertyName));
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Failure, Status.Success};
			bool hasValues = blackboard.TryGetClassValue(m_firstPropertyName, out T firstValue) &
				blackboard.TryGetClassValue(m_secondPropertyName, out T secondValue);
			bool equals = Equals(firstValue, secondValue);
			int index = *(byte*)&hasValues << *(byte*)&equals;

			return results[index];
		}
	}
}
