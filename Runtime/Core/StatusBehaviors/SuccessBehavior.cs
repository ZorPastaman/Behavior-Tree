// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.StatusBehaviors
{
	public sealed class SuccessBehavior : Behavior
	{
		public SuccessBehavior([NotNull] Blackboard blackboard) : base(blackboard)
		{
		}

		protected override Status Execute()
		{
			return Status.Success;
		}
	}
}
