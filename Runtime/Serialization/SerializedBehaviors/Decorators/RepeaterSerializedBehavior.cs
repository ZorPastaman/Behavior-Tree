// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	public sealed class RepeaterSerializedBehavior : SerializedBehavior<Repeater>
	{
		[SerializeField] private uint m_Repeats;

		public override object[] serializedCustomData
		{
			[Pure]
			get => new object[] {m_Repeats};
		}
	}
}
