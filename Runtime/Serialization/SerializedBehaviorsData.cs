// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.Serialization
{
	[Serializable]
	public struct SerializedBehaviorsData
	{
		public SerializedBehavior serializedBehavior;
		public int[] childrenIndices;
	}
}
