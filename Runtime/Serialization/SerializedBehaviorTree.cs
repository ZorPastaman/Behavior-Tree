// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.StatusBehaviors;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Serialization
{
	[CreateAssetMenu(
		menuName = "Behavior Tree/Serialized Behavior Tree",
		fileName = "Serialized Behavior Tree",
		order = 448
	)]
	public sealed class SerializedBehaviorTree : ScriptableObject
	{
		private TreeBuilder m_treeBuilder;

		public TreeRoot CreateTree()
		{
			return CreateTree(new Blackboard());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeRoot CreateTree([NotNull] Blackboard blackboard)
		{
			return m_treeBuilder.Build(blackboard);
		}

		private void OnEnable()
		{
			m_treeBuilder = new TreeBuilder();
			m_treeBuilder.AddBehavior<SuccessBehavior>().Finish();
		}
	}
}
