﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Clip Property Name", 0), NameOverride("Audio Property Name", 1)]
	[SearchGroup("Audio")]
	public sealed class SerializedPlayOneShotAudioSourceVariable :
		SerializedAction<PlayOneShotAudioSourceVariable, string, string>
	{
	}
}
