// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("X Property Name", 1),
	NameOverride("Y Property Name", 2), NameOverride("Z Property Name", 3)]
	[SearchGroup("Vector3")]
	public sealed class SerializedGetVector3Coordinates :
		SerializedAction<GetVector3Coordinates, string, string, string, string>
	{
	}
}
