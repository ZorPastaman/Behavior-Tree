// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Core.Leaves.StatusBehaviors
{
	/// <summary>
	/// Always returns <see cref="Status.Failure"/>.
	/// </summary>
	public sealed class FailureBehavior : StatusBehavior, INotSetupable
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		protected override Status Execute()
		{
			return Status.Failure;
		}
	}
}
