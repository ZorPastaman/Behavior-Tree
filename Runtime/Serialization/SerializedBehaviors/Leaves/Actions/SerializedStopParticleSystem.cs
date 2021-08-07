// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Particle System Property Name", 0), NameOverride("With Children", 1),
	NameOverride("Stop Behavior", 2)]
	[SearchGroup("Particle System")]
	public sealed class SerializedStopParticleSystem :
		SerializedAction<StopParticleSystem, string, bool, ParticleSystemStopBehavior>
	{
	}
}
