// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using UnityEngine;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors
{
	public abstract class SerializedBehavior : ScriptableObject
	{
		public abstract (Type, object[]) GetSerializedData();
	}
}
