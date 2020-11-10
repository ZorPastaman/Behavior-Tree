// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Zor.BehaviorTree.Core.Decorators
{
	[UsedImplicitly, Preserve]
	public sealed class Inverter : Decorator
	{
		protected override unsafe Status Execute()
		{
			Status childStatus = child.Tick();

			Status* results = stackalloc Status[] {childStatus, Status.Failure, Status.Success};
			int index = ((int)(childStatus & Status.Success) >> 1) | ((int)(childStatus & Status.Failure) >> 2);

			return results[index];
		}
	}
}
