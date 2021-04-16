// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class HasStructValue<T> : Condition,
		ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : struct
	{
		private BlackboardPropertyName m_propertyName;

		public void Setup(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		public void Setup([NotNull] string propertyName)
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
