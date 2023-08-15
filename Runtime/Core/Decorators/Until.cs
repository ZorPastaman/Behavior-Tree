// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// This <see cref="Decorator"/> ticks its child and returns its result
	/// but instead of <see cref="Status.Failure"/> this returns <see cref="Status.Running"/>.
	/// </summary>
	public sealed class Until : Decorator, INotSetupable
	{
		protected override Status Execute()
		{
			Status childStatus = child.Tick();
			return StateToStatusHelper.ConditionToStatus(childStatus == Status.Failure, childStatus, Status.Running);
		}
	}
}
