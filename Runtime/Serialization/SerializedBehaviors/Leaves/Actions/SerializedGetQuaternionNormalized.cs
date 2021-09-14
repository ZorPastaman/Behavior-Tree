// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Quaternion Property Name", 0), NameOverride("Normalized Property Name", 1)]
	[SearchGroup("Quaternion")]
	public sealed class SerializedGetQuaternionNormalized : SerializedAction<GetQuaternionNormalized, string, string>
	{
	}
}
