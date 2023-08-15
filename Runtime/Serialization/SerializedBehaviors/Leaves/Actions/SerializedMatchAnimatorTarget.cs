// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Animator Property Name", 0), NameOverride("Position Property Name", 1),
	NameOverride("Rotation Property Name", 2), NameOverride("Avatar Target", 3),
	NameOverride("Position XYZ Weight", 4), NameOverride("Rotation Weight", 5),
	NameOverride("Start Normalized Time", 6)]
	[SearchGroup("Animator")]
	public sealed class SerializedMatchAnimatorTarget :
		SerializedAction<MatchAnimatorTarget, string, string, string, AvatarTarget, Vector3, float, float>
	{
	}
}
