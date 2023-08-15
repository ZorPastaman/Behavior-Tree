// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// <see cref="BehaviorTreeAgentTicker"/> that calls a tick every Late update.
	/// </summary>
	/// <seealso cref="UpdateBehaviorTreeAgentTicker"/>
	/// <seealso cref="FixedUpdateBehaviorTreeAgentTicker"/>
	[AddComponentMenu("Behavior Tree/Tickers/Late Update Behavior Tree Agent Ticker")]
	public sealed class LateUpdateBehaviorTreeAgentTicker : BehaviorTreeAgentTicker
	{
		private void LateUpdate()
		{
			Tick();
		}
	}
}
