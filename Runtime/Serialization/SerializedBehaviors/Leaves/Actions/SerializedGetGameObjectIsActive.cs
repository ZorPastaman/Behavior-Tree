// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Game Object Property Name", 0), NameOverride("Is Active Property Name", 1)]
	[SearchGroup("Game Object")]
	public sealed class SerializedGetGameObjectIsActive : SerializedAction<GetGameObjectIsActive, string, string>
	{
	}
}
