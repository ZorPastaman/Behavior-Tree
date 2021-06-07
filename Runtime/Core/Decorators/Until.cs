// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class Until : Decorator, INotSetupable
	{
		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();
			Status* results = stackalloc Status[] {childStatus, Status.Running};
			int index = (int)(childStatus & Status.Failure) >> 3;

			return results[index];
		}
	}
}
