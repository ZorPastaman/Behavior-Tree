﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Particle System Property Name", 0), NameOverride("With Children", 1)]
	[SearchGroup("Particle System")]
	public sealed class SerializedIsParticleSystemAlive : SerializedCondition<IsParticleSystemAlive, string, bool>
	{
	}
}
