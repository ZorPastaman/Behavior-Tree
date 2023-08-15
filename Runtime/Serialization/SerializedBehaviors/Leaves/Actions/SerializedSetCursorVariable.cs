// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Texture Property Name", 0), NameOverride("Hotspot Property Name", 1),
	NameOverride("Cursor Mode Property Name", 2)]
	[SearchGroup("Cursor")]
	public sealed class SerializedSetCursorVariable : SerializedAction<SetCursorVariable, string, string, string>
	{
	}
}
