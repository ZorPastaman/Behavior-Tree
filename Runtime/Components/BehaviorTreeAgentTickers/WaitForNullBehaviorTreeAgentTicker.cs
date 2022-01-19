// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// <see cref="CoroutineBehaviorTreeAgentTicker"/> that uses <see langword="null"/>.
	/// </summary>
	/// <seealso cref="WaitForSecondsBehaviorTreeAgentTicker"/>
	/// <seealso cref="WaitForFixedUpdateBehaviorTreeAgentTicker"/>
	[AddComponentMenu("Behavior Tree/Tickers/Wait For Null Behavior Tree Agent Ticker")]
	public sealed class WaitForNullBehaviorTreeAgentTicker : CoroutineBehaviorTreeAgentTicker
	{
		protected override YieldInstruction instruction => null;
	}
}
