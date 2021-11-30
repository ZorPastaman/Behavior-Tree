// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Profiling;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Debugging;
using Zor.BehaviorTree.Serialization;
using Zor.SimpleBlackboard.Components;

namespace Zor.BehaviorTree.Components
{
	[AddComponentMenu("Behavior Tree/Behavior Tree Agent")]
	public sealed class BehaviorTreeAgent : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private SerializedBehaviorTree_Base m_SerializedBehaviorTree;
		[SerializeField] private SimpleBlackboardContainer m_BlackboardContainer;
#pragma warning restore CS0649

		private TreeRoot m_treeRoot;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Status Tick()
		{
			Profiler.BeginSample(gameObject.name);

			Status status = m_treeRoot.Tick();

#if DEBUG
			if (status == Status.Error)
			{
				BehaviorTreeDebug.LogError(this, $"[BehaviorTreeAgent] Behavior tree at {gameObject.name} finished a tick with an error");
			}
#endif

			Profiler.EndSample();

			return status;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Abort")]
		public Status Abort()
		{
			Profiler.BeginSample(gameObject.name);

			Status status = m_treeRoot.Abort();

			Profiler.EndSample();

			return status;
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

		[ContextMenu("Recreate Tree")]
		private void RecreateTree()
		{
			Awake();
		}
	}
}
