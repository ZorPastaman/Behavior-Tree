// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Conditions
{
	[UsedImplicitly, Preserve]
	public sealed class IsClassEqual<T> : Behavior where T : class, IEquatable<T>
	{
		private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public IsClassEqual([CanBeNull] T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		public IsClassEqual([CanBeNull] T value, [NotNull] string propertyName)
		{
			m_value = value;
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override unsafe Status Execute()
		{
			if (blackboard.TryGetClassValue(m_propertyName, out T currentValue))
			{
				Status* results = stackalloc Status[] {Status.Failure, Status.Success};
				bool equals = m_value.Equals(currentValue);
				byte index = *(byte*)&equals;

				return results[index];
			}

			return Status.Error;
		}
	}
}
