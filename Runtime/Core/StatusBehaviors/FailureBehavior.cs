// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Zor.BehaviorTree.Core.StatusBehaviors
{
	[UsedImplicitly, Preserve]
	public sealed class FailureBehavior : Behavior
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			return Status.Failure;
		}
	}
}
