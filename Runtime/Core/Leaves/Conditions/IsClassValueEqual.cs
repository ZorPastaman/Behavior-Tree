// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsClassValueEqual<T> : Condition, ISetupable<T, BlackboardPropertyName>, ISetupable<T, string>
		where T : class
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
		public void Setup([CanBeNull] T value, [NotNull] string propertyName)
		{
			Setup(value, new BlackboardPropertyName(propertyName));
		}

		[Pure]
		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Failure, Status.Success};
			bool hasValue = blackboard.TryGetClassValue(m_propertyName, out T currentValue);
			bool equals = Equals(m_value, currentValue);
			int index = *(byte*)&hasValue << *(byte*)&equals;

			return results[index];
		}
	}
}
