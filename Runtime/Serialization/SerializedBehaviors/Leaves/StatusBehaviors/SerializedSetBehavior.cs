// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.StatusBehaviors
{
	[NameOverride("Status", 0)]
	public sealed class SerializedSetBehavior : SerializedStatusBehavior<SetStatusBehavior, Status>
	{
	}
}
