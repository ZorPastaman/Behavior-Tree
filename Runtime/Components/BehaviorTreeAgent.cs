// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Profiling;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Debugging;
using Zor.BehaviorTree.Serialization;
using Zor.SimpleBlackboard.Components;

namespace Zor.BehaviorTree.Components
{
	/// <summary>
	/// Creator and holder of a behavior tree.
	/// </summary>
	/// <remarks>
	/// <para>Agent deserializes a tree on Awake.</para>
	/// <para>Agent disposes a tree on OnDestroy.</para>
	/// </remarks>
	[AddComponentMenu("Behavior Tree/Behavior Tree Agent")]
	public sealed class BehaviorTreeAgent : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField, Tooltip("Serialized behavior tree. It's automatically deserialized on Awake.")]
		private SerializedBehaviorTree_Base m_SerializedBehaviorTree;
		[SerializeField, Tooltip("Blackboard container. It's used as a blackboard for the behavior tree.")]
		private SimpleBlackboardContainer m_BlackboardContainer;
#pragma warning restore CS0649

		/// <summary>
		/// <see cref="TreeRoot"/> of the deserialized and used behavior tree.
		/// </summary>
		private TreeRoot m_treeRoot;

		/// <summary>
		/// Calls <see cref="TreeRoot.Tick"/> of the internal behavior tree and returns its result.
		/// </summary>
		/// <returns>Result of the tick.</returns>
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

		/// <summary>
		/// Calls <see cref="TreeRoot.Abort"/> of the internal behavior tree and returns its result.
		/// </summary>
		/// <returns>Result of the abort.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Abort")]
		public Status Abort()
		{
			Profiler.BeginSample(gameObject.name);

			Status status = m_treeRoot.Abort();

			Profiler.EndSample();

			return status;
		}

		/// <summary>
		/// Recreates a tree with the same serialized tree.
		/// </summary>
		/// <remarks>
		/// This doesn't return a serialized tree or a blackboard to their original states.
		/// </remarks>
		[ContextMenu("Recreate Tree")]
		public void RecreateTree()
		{
			Awake();
		}

		private void Awake()
		{
#if DEBUG
			if (m_SerializedBehaviorTree == null)
			{
				BehaviorTreeDebug.LogError(this, "Serialized behavior tree is null");
				return;
			}

			if (m_BlackboardContainer == null)
			{
				BehaviorTreeDebug.LogError(this, "Blackboard container is null");
				return;
			}
#endif

			m_treeRoot = m_SerializedBehaviorTree.CreateTree(m_BlackboardContainer.blackboard);
			m_treeRoot.Initialize();
		}

		private void OnDestroy()
		{
			m_treeRoot?.Dispose();
		}
	}
}
