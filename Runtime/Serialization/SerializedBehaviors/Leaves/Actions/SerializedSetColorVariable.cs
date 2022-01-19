// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Red Property Name", 0), NameOverride("Green Property Name", 1),
	NameOverride("Blue Property Name", 2), NameOverride("Alpha Property Name", 3),
	NameOverride("Color Property Name", 4)]
	[SearchGroup("Color")]
	public sealed class SerializedSetColorVariable :
		SerializedAction<SetColorVariable, string, string, string, string, string>
	{
	}
}
