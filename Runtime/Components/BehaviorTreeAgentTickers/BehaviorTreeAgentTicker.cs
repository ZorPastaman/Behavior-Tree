// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	public abstract class BehaviorTreeAgentTicker : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private BehaviorTreeAgent m_BehaviorTreeAgent;
#pragma warning restore CS0649

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void Tick()
		{
			m_BehaviorTreeAgent.Tick();
		}
	}
}
