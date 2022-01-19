// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// <see cref="CoroutineBehaviorTreeAgentTicker"/> that uses <see cref="WaitForSeconds"/>.
	/// </summary>
	/// <seealso cref="WaitForNullBehaviorTreeAgentTicker"/>
	/// <seealso cref="WaitForFixedUpdateBehaviorTreeAgentTicker"/>
	[AddComponentMenu("Behavior Tree/Tickers/Wait For Seconds Behavior Tree Agent Ticker")]
	public sealed class WaitForSecondsBehaviorTreeAgentTicker : CoroutineBehaviorTreeAgentTicker
	{
#pragma warning disable CS0649
		/// <summary>
		/// How many seconds the period is between each tick.
		/// </summary>
		[SerializeField] private float m_Seconds;
#pragma warning restore CS0649

		/// <summary>
		/// How many seconds the period is between each tick.
		/// </summary>
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
