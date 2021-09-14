// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	[AddComponentMenu("Behavior Tree/Tickers/Wait For Seconds Behavior Tree Agent Ticker")]
	public sealed class WaitForSecondsBehaviorTreeAgentTicker : CoroutineBehaviorTreeAgentTicker
	{
#pragma warning disable CS0649
		[SerializeField] private float m_Seconds;
#pragma warning restore CS0649

		public float seconds
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_Seconds;
			set
			{
				if (m_Seconds == value)
				{
					return;
				}

				m_Seconds = value;
				UpdateInstruction();
			}
		}

		protected override YieldInstruction instruction => new WaitForSeconds(m_Seconds);
	}
}
