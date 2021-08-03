// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class Until : Decorator, INotSetupable
	{
		protected override Status Execute()
		{
			Status childStatus = child.Tick();
			return StateToStatusHelper.ConditionToStatus(childStatus == Status.Failure, childStatus, Status.Running);
		}
	}
}
