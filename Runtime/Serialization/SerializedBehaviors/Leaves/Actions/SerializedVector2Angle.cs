// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("From Property Name", 0), NameOverride("To Property Name", 1),
	NameOverride("Angle Property Name", 2)]
	[SearchGroup("Vector2")]
	public sealed class SerializedVector2Angle : SerializedAction<Vector2Angle, string, string, string>
	{
	}
}
