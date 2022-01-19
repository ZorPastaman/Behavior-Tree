// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions
{
	[NameOverride("Lock State", 0)]
	[SearchGroup("Cursor")]
	public sealed class SerializedIsCursorLockStateEqual : SerializedCondition<IsCursorLockStateEqual, CursorLockMode>
	{
	}
}
