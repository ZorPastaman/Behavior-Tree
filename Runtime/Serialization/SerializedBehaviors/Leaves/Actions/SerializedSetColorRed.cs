// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Color Property Name", 0), NameOverride("Red", 1), NameOverride("Result Property Name", 2)]
	[SearchGroup("Color")]
	public sealed class SerializedSetColorRed : SerializedAction<SetColorRed, string, float, string>
	{
	}
}
