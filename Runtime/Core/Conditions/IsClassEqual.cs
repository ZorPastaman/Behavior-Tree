// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Conditions
{
	public sealed class IsClassEqual<T> : Behavior where T : class, IEquatable<T>
	{
		private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public IsClassEqual([NotNull] Blackboard blackboard, [CanBeNull] T value, BlackboardPropertyName propertyName)
			: base(blackboard)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_propertyName, out T currentValue))
			{
				return m_value.Equals(currentValue) ? Status.Success : Status.Failure;
			}

			return Status.Error;
		}
	}
}
