// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// This <see cref="Leaf"/> does something and returns a result.
	/// </para>
	/// <para>
	/// Usually, this behavior returns <see cref="Status.Success"/> when it finishes its action,
	/// <see cref="Status.Failure"/> when it fails or it's impossible to complete its action,
	/// or <see cref="Status.Running"/> when it does something successfully but it requires more ticks to finish.
	/// </para>
	/// </summary>
	public abstract class Action : Leaf
	{
	}
}
