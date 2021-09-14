// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	[AddComponentMenu("Behavior Tree/Tickers/Fixed Update Behavior Tree Agent Ticker")]
	public sealed class FixedUpdateBehaviorTreeAgentTicker : BehaviorTreeAgentTicker
	{
		private void FixedUpdate()
		{
			Tick();
		}
	}
}
