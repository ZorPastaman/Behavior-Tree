// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Core;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Serialization
{
	public abstract class SerializedBehaviorTree_Base : ScriptableObject
	{
		[NotNull]
		public TreeRoot CreateTree()
		{
			return CreateTree(new Blackboard());
		}

		[NotNull]
		public abstract TreeRoot CreateTree([NotNull] Blackboard blackboard);
	}
}
