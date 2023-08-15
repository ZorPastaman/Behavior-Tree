// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("Y Property Name", 1),
	NameOverride("Result Property Name", 2)]
	[SearchGroup("Vector2")]
	public sealed class SerializedSetVector2YVariable : SerializedAction<SetVector2YVariable, string, string, string>
	{
	}
}
