// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.StatusBehaviors
{
	[UsedImplicitly, Preserve]
	public sealed class VariableBehavior : Behavior
	{
		private readonly BlackboardPropertyName m_variableName;

		public VariableBehavior(BlackboardPropertyName variableName)
		{
			m_variableName = variableName;
		}

		public VariableBehavior([NotNull] string variableName)
		{
			m_variableName = new BlackboardPropertyName(variableName);
		}

		protected override unsafe Status Execute()
		{
			Status* results = stackalloc Status[] {Status.Error, Status.Error};
			bool found = blackboard.TryGetStructValue(m_variableName, out results[1]);
			byte index = *(byte*)&found;

			return results[index];
		}
	}
}
