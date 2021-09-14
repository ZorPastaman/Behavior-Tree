// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("X", 0), NameOverride("Y", 1), NameOverride("Z", 2), NameOverride("W", 3)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedSetQuaternion : SerializedAction<SetQuaternion, float, float, float, float, string>
	{
	}
}
