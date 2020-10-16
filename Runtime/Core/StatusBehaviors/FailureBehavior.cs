// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.StatusBehaviors
{
	[UsedImplicitly, Preserve]
	public sealed class FailureBehavior : Behavior
	{
		public FailureBehavior([NotNull] Blackboard blackboard) : base(blackboard)
		{
		}

		protected override Status Execute()
		{
			return Status.Failure;
		}
	}
}
