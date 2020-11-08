// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Conditions
{
	[UsedImplicitly, Preserve]
	public sealed class HasClassValue<T> : Behavior where T : class
	{
		private readonly BlackboardPropertyName m_propertyName;

		public HasClassValue(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		public HasClassValue([NotNull] string propertyName)
		{
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Failure, Status.Success};
			bool hasValue = blackboard.ContainsObjectValue<T>(m_propertyName);
			int index = *(int*)&hasValue;

			return results[index];
		}
	}
}
