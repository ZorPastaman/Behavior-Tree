// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Bounds Property Name", 0), NameOverride("Point Property Name", 1), NameOverride("Distance", 2)]
	[SearchGroup("Bounds")]
	public sealed class SerializedIsBoundsDistanceLess :
		SerializedCondition<IsBoundsDistanceLess, string, string, float>
	{
	}
}
