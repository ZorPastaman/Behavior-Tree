// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Particle System Property Name", 0), NameOverride("Is Playing Property Name", 1)]
	[SearchGroup("Particle System")]
	public sealed class SerializedGetParticleSystemIsPlaying :
		SerializedAction<GetParticleSystemIsPlaying, string, string>
	{
	}
}
