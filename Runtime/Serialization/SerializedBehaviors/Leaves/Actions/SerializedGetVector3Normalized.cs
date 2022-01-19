// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Vector Property Name", 0), NameOverride("Normalized Property Name", 1)]
	[SearchGroup("Vector3")]
	public sealed class SerializedGetVector3Normalized : SerializedAction<GetVector3Normalized, string, string>
	{
	}
}
