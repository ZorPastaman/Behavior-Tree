// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class Not : Decorator
	{
		public Not([NotNull] Blackboard blackboard, [NotNull] Behavior child) : base(blackboard, child)
		{
		}

		protected override Status Execute()
		{
			Status childStatus = child.Tick();

			switch (childStatus)
			{
				case Status.Success:
					return Status.Failure;
				case Status.Failure:
					return Status.Success;
				default:
					return childStatus;
			}
		}
	}
}
