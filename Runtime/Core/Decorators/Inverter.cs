// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// This <see cref="Decorator"/> ticks its child and inverts the result.
	/// <see cref="Status.Success"/> becomes <see cref="Status.Failure"/>.
	/// <see cref="Status.Failure"/> becomes <see cref="Status.Success"/>.
	/// Other results remain as they are.
	/// </summary>
	public sealed class Inverter : Decorator, INotSetupable
	{
		protected override Status Execute()
		{
			Status childStatus = child.Tick();
			int midStatus = (int)(childStatus & Status.Success) << 2 | (int)(childStatus & Status.Failure) >> 2;

			return StateToStatusHelper.ConditionToStatus(midStatus == 0, (Status)midStatus, childStatus);
		}
	}
}
