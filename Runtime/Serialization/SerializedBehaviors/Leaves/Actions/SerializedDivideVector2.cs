// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Left Operand Property Name", 0), NameOverride("Right Operand Property Name", 1),
	NameOverride("Result Property Name", 2)]
	[SearchGroup("Vector2")]
	public sealed class SerializedDivideVector2 : SerializedAction<DivideVector2, string, string, string>
	{
	}
}
