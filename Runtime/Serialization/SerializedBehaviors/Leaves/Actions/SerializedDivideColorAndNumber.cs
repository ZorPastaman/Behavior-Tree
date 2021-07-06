// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Color Property Name", 0), NameOverride("Number", 1),
	NameOverride("Result Property Name", 2)]
	[SearchGroup("Color")]
	public sealed class SerializedDivideColorAndNumber : SerializedAction<DivideColorAndNumber, string, float, string>
	{
	}
}
