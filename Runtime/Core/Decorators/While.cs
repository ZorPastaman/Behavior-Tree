// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class While : Decorator, INotSetupable
	{
		protected override Status Execute()
		{
			Status childStatus = child.Tick();
			int midStatus = (int)(childStatus & Status.Success) << 1 | (int)(childStatus & Status.Failure) >> 2;

			return StateToStatusHelper.ConditionToStatus(midStatus == 0, (Status)midStatus, childStatus);
		}
	}
}
