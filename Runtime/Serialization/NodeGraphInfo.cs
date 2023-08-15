// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Serialization
{
	/// <summary>
	/// View settings of a behavior node in a behavior tree graph view.
	/// </summary>
	[Serializable]
	public sealed class NodeGraphInfo
	{
		/// <summary>
		/// Position of a node.
		/// </summary>
		[UsedImplicitly] public Vector2 position;
	}
}
