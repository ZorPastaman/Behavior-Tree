// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Quaternion Property Name", 0), NameOverride("W", 1), NameOverride("Result Property Name", 2)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedSetQuaternionW : SerializedAction<SetQuaternionW, string, float, string>
	{
	}
}
