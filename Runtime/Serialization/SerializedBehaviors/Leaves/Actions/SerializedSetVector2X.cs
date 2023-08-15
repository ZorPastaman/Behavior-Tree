// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("X", 1), NameOverride("Result Property Name", 2)]
	[SearchGroup("Vector2")]
	public sealed class SerializedSetVector2X : SerializedAction<SetVector2X, string, float, string>
	{
	}
}
