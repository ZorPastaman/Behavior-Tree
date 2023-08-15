// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// <see cref="BehaviorTreeAgentTicker"/> that calls a tick every Fixed update.
	/// </summary>
	/// <seealso cref="UpdateBehaviorTreeAgentTicker"/>
	/// <seealso cref="LateUpdateBehaviorTreeAgentTicker"/>
	[AddComponentMenu("Behavior Tree/Tickers/Fixed Update Behavior Tree Agent Ticker")]
	public sealed class FixedUpdateBehaviorTreeAgentTicker : BehaviorTreeAgentTicker
	{
		private void FixedUpdate()
		{
			Tick();
		}
	}
}
