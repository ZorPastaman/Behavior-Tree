// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Start Property Name", 0), NameOverride("End Property Name", 1),
	NameOverride("Radius Property Name", 2), NameOverride("Layer Mask Property Name", 3)]
	[SearchGroup("Physics")]
	public sealed class SerializedCheckCapsuleVariable :
		SerializedCondition<CheckCapsuleVariable, string, string, string, string>
	{
	}
}
