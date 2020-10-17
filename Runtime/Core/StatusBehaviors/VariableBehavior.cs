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

		public VariableBehavior([NotNull] Blackboard blackboard, BlackboardPropertyName variableName) : base(blackboard)
		{
			m_variableName = variableName;
		}

		protected override Status Execute()
		{
			return blackboard.TryGetStructValue(m_variableName, out Status variableStatus)
				? variableStatus
				: Status.Error;
		}
	}
}
