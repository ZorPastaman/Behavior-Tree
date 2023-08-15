// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Origin Property Name", 0), NameOverride("End Property Name", 1),
	NameOverride("LayerMask Property Name", 2)]
	[SearchGroup("Physics")]
	public sealed class SerializedLinecastVariable : SerializedCondition<LinecastVariable, string, string, string>
	{
	}
}
