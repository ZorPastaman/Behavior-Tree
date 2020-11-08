// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Conditions
{
	[UsedImplicitly, Preserve]
	public sealed class IsStructEqual<T> : Behavior where T : struct, IEquatable<T>
	{
		private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public IsStructEqual(T value, BlackboardPropertyName propertyName)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		public IsStructEqual(T value, [NotNull] string propertyName)
		{
			m_value = value;
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override unsafe Status Execute()
		{
			if (blackboard.TryGetStructValue(m_propertyName, out T currentValue))
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
