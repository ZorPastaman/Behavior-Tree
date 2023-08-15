// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Color Property Name", 0), NameOverride("Red Property Name", 1)]
	[SearchGroup("Color")]
	public sealed class SerializedGetColorRed : SerializedAction<GetColorRed, string, string>
	{
	}
}
