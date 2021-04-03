// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.StatusBehaviors;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.StatusBehaviors
{
	public sealed class SuccessSerializedBehavior : SerializedBehavior
	{
		public override (Type, object[]) GetSerializedData()
		{
			return (typeof(SuccessBehavior), null);
		}
	}
}
