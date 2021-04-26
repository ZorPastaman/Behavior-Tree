// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;
using Zor.BehaviorTree.EditorTools;
using Zor.BehaviorTree.Helpers;
using Action = Zor.BehaviorTree.Core.Leaves.Actions.Action;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
	{
		private static readonly Comparison<Type> s_behaviorTypeComparer = (left, right) =>
			string.CompareOrdinal(left.Name, right.Name);

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

			var compositeTypes = new List<Type>();
			var decoratorTypes = new List<Type>();
			var leafTypes = new List<Type>();
			var actionTypes = new List<Type>();
			var conditionTypes = new List<Type>();
			var statusBehaviorTypes = new List<Type>();

			for (int i = 0, count = behaviorTypes.Length; i < count; ++i)
			{
				Type behaviorType = behaviorTypes[i];

				if (behaviorType.IsSubclassOf(typeof(Composite)))
				{
					compositeTypes.Add(behaviorType);
				}
				else if (behaviorType.IsSubclassOf(typeof(Decorator)))
				{
					decoratorTypes.Add(behaviorType);
				}
				else if (behaviorType.IsSubclassOf(typeof(Leaf)))
				{
					if (behaviorType.IsSubclassOf(typeof(Action)))
					{
						actionTypes.Add(behaviorType);
					}
					else if (behaviorType.IsSubclassOf(typeof(Condition)))
					{
						conditionTypes.Add(behaviorType);
					}
					else if (behaviorType.IsSubclassOf(typeof(StatusBehavior)))
					{
						statusBehaviorTypes.Add(behaviorType);
					}
					else
					{
						leafTypes.Add(behaviorType);
					}
				}
			}

			tree.Add(new SearchTreeGroupEntry(new GUIContent("Composites"), 1));
			AddEntries(tree, compositeTypes, 2);
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Decorators"), 1));
			AddEntries(tree, decoratorTypes, 2);
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Leaves"), 1));
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Actions"), 2));
			AddEntries(tree, actionTypes, 3);
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Conditions"), 2));
			AddEntries(tree, conditionTypes, 3);
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Status Behaviors"), 2));
			AddEntries(tree, statusBehaviorTypes, 3);
			AddEntries(tree, leafTypes, 3);

			return tree;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
		{
			m_graph.CreateNewBehavior((Type)searchTreeEntry.userData, context.screenMousePosition);
			return true;
		}

		private void AddEntries([NotNull] List<SearchTreeEntry> tree, [NotNull] List<Type> behaviorTypes, int level)
		{
			for (int i = 0, count = behaviorTypes.Count; i < count; ++i)
			{
				Type behaviorType = behaviorTypes[i];

				SerializedBehaviorsCollection.TryGetSerializedBehaviorType(behaviorType,
					out Type serializedBehaviorType);
				tree.Add(new SearchTreeEntry(new GUIContent(TypeHelper.GetUIName(behaviorType)))
				{
					level = level,
					userData = serializedBehaviorType
				});
			}
		}
	}
}
