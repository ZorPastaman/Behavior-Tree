// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Color Property Name", 0), NameOverride("Red Property Name", 1),
	NameOverride("Result Property Name", 2)]
	[SearchGroup("Color")]
	public sealed class SerializedSetColorRedVariable : SerializedAction<SetColorRedVariable, string, string, string>
	{
	}
}
