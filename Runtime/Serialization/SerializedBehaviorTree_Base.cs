// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Profiling;
using Zor.BehaviorTree.Core;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Serialization
{
	/// <summary>
	/// Base class for serialized behavior tree.
	/// </summary>
	public abstract class SerializedBehaviorTree_Base : ScriptableObject
	{
		/// <summary>
		/// Creates a behavior tree out of its serialized data with a new <see cref="Blackboard"/>.
		/// </summary>
		/// <returns>Root of the created behavior tree.</returns>
		[NotNull]
		public TreeRoot CreateTree()
		{
			Profiler.BeginSample("SerializedBehaviorTree.CreateTree()");

			TreeRoot treeRoot = CreateTree(new Blackboard());

			Profiler.EndSample();

			return treeRoot;
		}

		/// <summary>
		/// Creates a behavior tree out of its serialized data.
		/// </summary>
		/// <param name="blackboard">Behavior tree blackboard.</param>
		/// <returns>Root of the created behavior tree.</returns>
		[NotNull]
		public abstract TreeRoot CreateTree([NotNull] Blackboard blackboard);
	}
}
