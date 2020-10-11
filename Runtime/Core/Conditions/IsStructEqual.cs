// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Conditions
{
	public sealed class IsStructEqual<T> : Behavior where T : struct, IEquatable<T>
	{
		private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public IsStructEqual([NotNull] Blackboard blackboard, T value, BlackboardPropertyName propertyName)
			: base(blackboard)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_propertyName, out T currentValue))
			{
				return m_value.Equals(currentValue) ? Status.Success : Status.Failure;
			}

			return Status.Error;
		}
	}
}
