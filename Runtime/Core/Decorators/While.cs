// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class While : Decorator, INotSetupable
	{
		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();
			Status* results = stackalloc Status[] {childStatus, Status.Running, Status.Success};
			int index = ((int)(childStatus & Status.Success) >> 1) | ((int)(childStatus & Status.Failure) >> 2);

			return results[index];
		}
	}
}
