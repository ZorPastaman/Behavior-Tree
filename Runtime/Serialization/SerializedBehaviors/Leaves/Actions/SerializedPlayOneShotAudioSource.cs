// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Clip", 0), NameOverride("Audio Property Name", 1)]
	[SearchGroup("Audio")]
	public sealed class SerializedPlayOneShotAudioSource : SerializedAction<PlayOneShotAudioSource, AudioClip, string>
	{
	}
}
