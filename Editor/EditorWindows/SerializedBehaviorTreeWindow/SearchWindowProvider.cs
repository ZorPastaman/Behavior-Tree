// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zor.BehaviorTree.EditorTools;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
	{
		private SerializedBehaviorTreeGraph m_graph;

		public void Initialize([NotNull] SerializedBehaviorTreeGraph graph)
		{
			m_graph = graph;
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
		{
			var tree = new List<SearchTreeEntry>
			{
				new SearchTreeGroupEntry(new GUIContent("Create Node"))
			};

			Type[] behaviorTypes = SerializedBehaviorsCollection.GetBehaviorTypes();

			for (int i = 0, count = behaviorTypes.Length; i < count; ++i)
			{
				Type behaviorType = behaviorTypes[i];
				SerializedBehaviorsCollection.TryGetSerializedBehaviorType(behaviorType,
					out Type serializedBehaviorType);
				tree.Add(new SearchTreeEntry(new GUIContent(behaviorType.Name))
				{
					level = 1,
					userData = serializedBehaviorType
				});
			}

			return tree;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
		{
			m_graph.CreateNewBehavior((Type)searchTreeEntry.userData, context.screenMousePosition);
			return true;
		}
	}
}
