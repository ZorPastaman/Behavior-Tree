// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Texture", 0), NameOverride("Hotspot", 1), NameOverride("Cursor Mode", 2)]
	[SearchGroup("Cursor")]
	public sealed class SerializedSetCursor : SerializedAction<SetCursor, Texture2D, Vector2, CursorMode>
	{
	}
}
