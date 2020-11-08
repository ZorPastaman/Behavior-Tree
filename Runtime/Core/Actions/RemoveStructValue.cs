// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Actions
{
	[UsedImplicitly, Preserve]
	public sealed class RemoveStructValue<T> : Behavior where T : struct
	{
		private readonly BlackboardPropertyName m_propertyName;

		public RemoveStructValue([NotNull] Blackboard blackboard, BlackboardPropertyName propertyName)
			: base(blackboard)
		{
			m_propertyName = propertyName;
		}

		public RemoveStructValue([NotNull] Blackboard blackboard, [NotNull] string propertyName)
			: base(blackboard)
		{
			m_propertyName = new BlackboardPropertyName(propertyName);
		}

		protected override Status Execute()
		{
			blackboard.RemoveStruct<T>(m_propertyName);
			return Status.Success;
		}
	}
}
