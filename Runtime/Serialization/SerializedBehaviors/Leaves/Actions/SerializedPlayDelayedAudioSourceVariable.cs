// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Audio Property Name", 0), NameOverride("Delay Property Name", 1)]
	[SearchGroup("Audio")]
	public sealed class SerializedPlayDelayedAudioSourceVariable :
		SerializedAction<PlayDelayedAudioSourceVariable, string, string>
	{
	}
}
