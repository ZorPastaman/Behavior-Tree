// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("From Color Property Name", 0), NameOverride("To Color Property Name", 1),
	NameOverride("Time", 2), NameOverride("Result Property Name", 3)]
	[SearchGroup("Color")]
	public sealed class SerializedLerpColor : SerializedAction<LerpColor, string, string, float, string>
	{
	}
}
