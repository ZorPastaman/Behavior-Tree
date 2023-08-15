// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow
{
	/// <summary>
	/// <see cref="Behavior"/> info for drawing it in ui.
	/// </summary>
	public struct BehaviorInfo
	{
		/// <summary>
		/// <see cref="Behavior"/>.
		/// </summary>
		public Behavior behavior;
		/// <summary>
		/// Behavior tree depth of the <see cref="behavior"/>. Root behavior must be of level 0.
		/// </summary>
		public int level;
	}
}
