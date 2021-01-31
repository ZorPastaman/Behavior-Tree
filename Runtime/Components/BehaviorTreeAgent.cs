// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Serialization;
using Zor.SimpleBlackboard.Components;

namespace Zor.BehaviorTree.Components
{
	[AddComponentMenu("Behavior Tree/Behavior Tree Agent")]
	public sealed class BehaviorTreeAgent : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private SerializedBehaviorTree m_SerializedBehaviorTree;
		[SerializeField] private SimpleBlackboardContainer m_BlackboardContainer;
#pragma warning restore CS0649

		private TreeRoot m_treeRoot;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Tick()
		{
			return m_treeRoot.Tick();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Abort()
		{
			return m_treeRoot.Abort();
		}

		private void Awake()
		{
			m_treeRoot = m_SerializedBehaviorTree.CreateTree(m_BlackboardContainer.blackboard);
			m_treeRoot.Initialize();
		}

		private void OnDestroy()
		{
			m_treeRoot?.Dispose();
		}
	}
}
