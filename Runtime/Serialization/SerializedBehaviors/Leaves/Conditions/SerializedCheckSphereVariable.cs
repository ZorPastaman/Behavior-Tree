// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Position Property Name", 0), NameOverride("Radius Property Name", 1),
	NameOverride("Layer Mask Property Name", 2)]
	[SearchGroup("Physics")]
	public sealed class SerializedCheckSphereVariable : SerializedCondition<CheckSphereVariable, string, string, string>
	{
	}
}
