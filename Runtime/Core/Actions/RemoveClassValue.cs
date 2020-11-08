// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	[UsedImplicitly, Preserve]
	public sealed class RemoveClassValue<T> : Behavior where T : class
	{
		private readonly BlackboardPropertyName m_propertyName;

		public RemoveClassValue(BlackboardPropertyName propertyName)
		{
			m_propertyName = propertyName;
		}

		public RemoveClassValue([NotNull] string propertyName)
		{
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override Status Execute()
		{
			blackboard.RemoveObject<T>(m_propertyName);
			return Status.Success;
		}
	}
}
