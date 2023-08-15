// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Euler Property Name", 0), NameOverride("Quaternion Property Name", 1)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedQuaternionEuler : SerializedAction<QuaternionEuler, string, string>
	{
	}
}
