// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	public sealed class SetClassValue<T> : Behavior where T : class
	{
		[CanBeNull] private readonly T m_value;
		private readonly BlackboardPropertyName m_propertyName;

		public SetClassValue([NotNull] Blackboard blackboard, [CanBeNull] T value, BlackboardPropertyName propertyName)
			: base(blackboard)
		{
			m_value = value;
			m_propertyName = propertyName;
		}

		protected override Status Execute()
		{
			blackboard.SetClassValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
