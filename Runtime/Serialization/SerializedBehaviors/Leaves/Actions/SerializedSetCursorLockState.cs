﻿// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.Core.Leaves.Actions;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions
{
	[NameOverride("Lock Mode", 0)]
	[SearchGroup("Cursor")]
	public sealed class SerializedSetCursorLockState : SerializedAction<SetCursorLockState, CursorLockMode>
	{
	}
}
