// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Conditions
{
	[UsedImplicitly, Preserve]
	public sealed class HasStructValue<T> : Behavior where T : struct
	{
		private readonly BlackboardPropertyName m_propertyName;

		public HasStructValue(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		public HasStructValue([NotNull] string propertyName)
		{
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Failure, Status.Success};
			bool hasValue = blackboard.ContainsStructValue<T>(m_propertyName);
			byte index = *(byte*)&hasValue;

			return results[index];
		}
	}
}