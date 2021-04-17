// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;

namespace Zor.BehaviorTree.Core.Leaves.StatusBehaviors
{
	public sealed class FailureBehavior : StatusBehavior, INotSetupable
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			return Status.Failure;
		}
	}
}
