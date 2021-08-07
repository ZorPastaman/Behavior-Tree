// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Particle System Property Name", 0), NameOverride("Is Paused Property Name", 1)]
	[SearchGroup("Particle System")]
	public sealed class SerializedGetParticleSystemIsPaused :
		SerializedAction<GetParticleSystemIsPaused, string, string>
	{
	}
}
