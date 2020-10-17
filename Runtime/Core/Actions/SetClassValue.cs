// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	[UsedImplicitly, Preserve]
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

		public SetClassValue([NotNull] Blackboard blackboard, [CanBeNull] T value, [NotNull] string propertyName)
			: base(blackboard)
		{
			m_value = value;
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override Status Execute()
		{
			blackboard.SetClassValue(m_propertyName, m_value);
			return Status.Success;
		}
	}
}
