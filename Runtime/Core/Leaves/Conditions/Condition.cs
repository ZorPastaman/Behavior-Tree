// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// This <see cref="Leaf"/> computes a condition and returns a result. It doesn't affect a game state.
	/// </para>
	/// <para>
	/// Usually, this behavior returns <see cref="Status.Success"/> if its condition is true,
	/// <see cref="Status.Failure"/> if its condition is false,
	/// or <see cref="Status.Running"/> if the condition requires some cycles to compute..
	/// </para>
	/// </summary>
	public abstract class Condition : Leaf
	{
	}
}
