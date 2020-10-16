// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Builder
{
	internal interface IBehaviorBuilder
	{
		Behavior Build([NotNull] Blackboard blackboard, [NotNull] Behavior[] children);
	}
}
