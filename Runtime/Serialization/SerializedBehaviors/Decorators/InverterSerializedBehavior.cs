// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	public sealed class InverterSerializedBehavior : SerializedBehavior
	{
		public override (Type, object[]) GetSerializedData()
		{
			return (typeof(Inverter), null);
		}
	}
}
