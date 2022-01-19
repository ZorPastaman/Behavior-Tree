// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("From Property Name", 0), NameOverride("To Property Name", 1),
	NameOverride("Max Degrees Delta", 2), NameOverride("Result Property Name", 3)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedQuaternionRotateTowards :
		SerializedAction<QuaternionRotateTowards, string, string, float, string>
	{
	}
}
