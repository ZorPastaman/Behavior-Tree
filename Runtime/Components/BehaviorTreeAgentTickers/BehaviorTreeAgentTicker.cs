// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	public abstract class BehaviorTreeAgentTicker : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private BehaviorTreeAgent m_BehaviorTreeAgent;
#pragma warning restore CS0649

		[NotNull]
		public BehaviorTreeAgent behaviorTreeAgent
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_BehaviorTreeAgent;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_BehaviorTreeAgent = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void Tick()
		{
			m_BehaviorTreeAgent.Tick();
		}

		protected virtual void OnEnable()
		{
			if (m_BehaviorTreeAgent == null)
			{
				enabled = false;
			}
		}

		protected virtual void OnValidate()
		{
		}
	}
}
