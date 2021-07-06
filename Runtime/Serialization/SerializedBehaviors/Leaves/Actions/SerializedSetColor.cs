// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Red", 0), NameOverride("Green", 1), NameOverride("Blue", 2), NameOverride("Alpha", 3),
	NameOverride("Color Property Name", 4)]
	[SearchGroup("Color")]
	public sealed class SerializedSetColor : SerializedAction<SetColor, float, float, float, float, string>
	{
	}
}
