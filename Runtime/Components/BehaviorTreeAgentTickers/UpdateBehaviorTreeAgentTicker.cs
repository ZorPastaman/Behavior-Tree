// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;

namespace Zor.BehaviorTree.Components.BehaviorTreeAgentTickers
{
	/// <summary>
	/// <see cref="BehaviorTreeAgentTicker"/> that calls a tick every Update.
	/// </summary>
	/// <seealso cref="LateUpdateBehaviorTreeAgentTicker"/>
	/// <seealso cref="FixedUpdateBehaviorTreeAgentTicker"/>
	[AddComponentMenu("Behavior Tree/Tickers/Update Behavior Tree Agent Ticker")]
	public sealed class UpdateBehaviorTreeAgentTicker : BehaviorTreeAgentTicker
	{
		private void Update()
		{
			Tick();
		}
	}
}
