// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Composites
{
	[NameOverride("Success Mode", 0), NameOverride("Failure Mode", 1)]
	public sealed class SerializedConcurrent : SerializedComposite<Concurrent, Concurrent.Mode, Concurrent.Mode>
	{
	}
}
