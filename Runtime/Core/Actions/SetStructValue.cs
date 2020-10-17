// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	[UsedImplicitly, Preserve]
	public sealed class SetStructValue<T> : Behavior where T : struct
	{
		private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public SetStructValue([NotNull] Blackboard blackboard, T value, BlackboardPropertyName propertyName)
			: base(blackboard)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		public SetStructValue([NotNull] Blackboard blackboard, T value, [NotNull] string propertyName)
			: base(blackboard)
		{
			m_value = value;
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override Status Execute()
		{
			blackboard.SetStructValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
