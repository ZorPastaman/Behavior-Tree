// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Serialization;
using Zor.SimpleBlackboard.Components;
using Tree = Zor.BehaviorTree.Core.Tree;

namespace Zor.BehaviorTree.Components
{
	[AddComponentMenu("Behavior Tree/Behavior Tree Agent")]
	public sealed class BehaviorTreeAgent : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private SerializedBehaviorTree m_SerializedBehaviorTree;
		[SerializeField] private BlackboardContainer m_BlackboardContainer;
#pragma warning restore CS0649

		private Tree m_tree;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Tick()
		{
			return m_tree.Tick();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Abort()
		{
			return m_tree.Abort();
		}

		private void Awake()
		{
			m_tree = m_SerializedBehaviorTree.CreateTree(m_BlackboardContainer.blackboard);
		}

		private void OnDestroy()
		{
			m_tree?.Dispose();
		}
	}
}
