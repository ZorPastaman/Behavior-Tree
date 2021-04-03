// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using UnityEngine;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	public sealed class RepeaterSerializedBehavior : SerializedBehavior
	{
		[SerializeField] private uint m_Repeats;

		public override (Type, object[]) GetSerializedData()
		{
			return (typeof(Repeater), new object[]{m_Repeats});
		}
	}
}
