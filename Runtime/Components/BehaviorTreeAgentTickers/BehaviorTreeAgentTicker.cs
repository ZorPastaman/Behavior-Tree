// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Debugging;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// Base class for a <see cref="BehaviorTreeAgent"/> ticker .
	/// </summary>
	public abstract class BehaviorTreeAgentTicker : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField, Tooltip("Ticked behavior tree agent.")]
		private BehaviorTreeAgent m_BehaviorTreeAgent;
		[SerializeField, Tooltip("If true, it disables itself if a tick returns Error.\nWorks only in Debug.")]
		private bool m_DisableOnError = true;
#pragma warning restore CS0649

		/// <summary>
		/// Ticked <see cref="BehaviorTreeAgent"/>.
		/// </summary>
		[NotNull]
		public BehaviorTreeAgent behaviorTreeAgent
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_BehaviorTreeAgent;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_BehaviorTreeAgent = value;
		}

		/// <summary>
		/// Ticks a <see cref="BehaviorTreeAgent"/> and returns its result.
		/// </summary>
		/// <returns>Result of the tick.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Status Tick()
		{
			Status status = m_BehaviorTreeAgent.Tick();

#if DEBUG
			if (m_DisableOnError & status == Status.Error)
			{
				enabled = false;
			}
#endif

			return status;
		}

		protected virtual void OnEnable()
		{
			if (m_BehaviorTreeAgent == null)
			{
				BehaviorTreeDebug.LogWarning(this, $"[BehaviorTreeAgentTicker] Null behavior tree agent at {gameObject.name}");
				enabled = false;
			}
		}

		protected virtual void OnValidate()
		{
		}
	}
}
