// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// This <see cref="Decorator"/> ticks its child and returns its result
	/// but instead of <see cref="Status.Success"/> this returns <see cref="Status.Running"/>
	/// and instead of <see cref="Status.Failure"/> this returns <see cref="Status.Success"/>.
	/// </summary>
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
