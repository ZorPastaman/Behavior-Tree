// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Origin Property Name", 0), NameOverride("Direction Property Name", 1),
	NameOverride("Max Distance", 2), NameOverride("Layer Mask Property Name", 3)]
	[SearchGroup("Physics")]
	public sealed class SerializedRaycastVariable : SerializedCondition<RaycastVariable, string, string, string, string>
	{
	}
}
