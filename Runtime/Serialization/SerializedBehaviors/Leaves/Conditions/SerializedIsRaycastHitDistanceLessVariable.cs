// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Raycast Hit Property Name", 0), NameOverride("Distance Property Name", 1)]
	[SearchGroup("Raycast Hit")]
	public sealed class SerializedIsRaycastHitDistanceLessVariable :
		SerializedCondition<IsRaycastHitDistanceLessVariable, string, string>
	{
	}
}
