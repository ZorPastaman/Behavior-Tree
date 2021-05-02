// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	public sealed class Until : Decorator, INotSetupable
	{
		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();
			Status* results = stackalloc Status[] {childStatus, Status.Running};
			bool isFailure = childStatus == Status.Failure;
			byte index = *(byte*)&isFailure;

			return results[index];
		}
	}
}
