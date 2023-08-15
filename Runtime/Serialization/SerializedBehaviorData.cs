// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.Serialization
{
	/// <summary>
	/// Serialized behavior data.
	/// </summary>
	[Serializable]
	public struct SerializedBehaviorData
	{
		/// <summary>
		/// Serialized behavior.
		/// </summary>
		public SerializedBehavior_Base serializedBehavior;
		/// <summary>
		/// Children indices. For decorators, only first element exists. For leaves, the array is empty.
		/// </summary>
		public int[] childrenIndices;
		/// <summary>
		/// Node graph info for drawing it in a behavior tree graph view.
		/// </summary>
		[UsedImplicitly] public NodeGraphInfo nodeGraphInfo;
	}
}
