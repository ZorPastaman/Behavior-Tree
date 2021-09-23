// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Rigidbody Property Name", 0), NameOverride("Force Property Name", 1),
	NameOverride("Force Mode Property Name", 2)]
	[SearchGroup("Rigidbody")]
	public sealed class SerializedRigidbodyAddRelativeForceVariable :
		SerializedAction<RigidbodyAddRelativeForceVariable, string, string, string>
	{
	}
}
