﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsCursorVisible : Condition, INotSetupable
	{
		[Pure]
		protected override Status Execute()
		{
			return StateToStatusHelper.ConditionToStatus(Cursor.visible);
		}
	}
}
