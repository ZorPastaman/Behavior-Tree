// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// <see cref="CoroutineBehaviorTreeAgentTicker"/> that uses <see cref="WaitForFixedUpdate"/>.
	/// </summary>
	/// <seealso cref="WaitForNullBehaviorTreeAgentTicker"/>
	/// <seealso cref="WaitForSecondsBehaviorTreeAgentTicker"/>
	[AddComponentMenu("Behavior Tree/Tickers/Wait For Fixed Update Behavior Tree Agent Ticker")]
	public sealed class WaitForFixedUpdateBehaviorTreeAgentTicker : CoroutineBehaviorTreeAgentTicker
	{
		protected override YieldInstruction instruction => new WaitForFixedUpdate();
	}
}
