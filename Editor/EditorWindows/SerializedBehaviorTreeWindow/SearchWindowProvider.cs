// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves;
using Zor.BehaviorTree.Core.Leaves.Conditions;
using Zor.BehaviorTree.Core.Leaves.StatusBehaviors;
using Zor.BehaviorTree.DrawingAttributes;
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
			Array.Sort(behaviorTypes, s_behaviorTypeComparer);

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

			var compositesGroup = new SearchTreeGroupEntry(new GUIContent("Composites"), 1);
			tree.Add(compositesGroup);
			AddEntries(tree, compositeTypes, compositesGroup);
			var decoratorsGroup = new SearchTreeGroupEntry(new GUIContent("Decorators"), 1);
			tree.Add(decoratorsGroup);
			AddEntries(tree, decoratorTypes, decoratorsGroup);
			var leavesGroup = new SearchTreeGroupEntry(new GUIContent("Leaves"), 1);
			tree.Add(leavesGroup);
			var actionsGroup = new SearchTreeGroupEntry(new GUIContent("Actions"), 2);
			tree.Add(actionsGroup);
			AddEntries(tree, actionTypes, actionsGroup);
			var conditionsGroup = new SearchTreeGroupEntry(new GUIContent("Conditions"), 2);
			tree.Add(conditionsGroup);
			AddEntries(tree, conditionTypes, conditionsGroup);
			var statusBehaviorsGroup = new SearchTreeGroupEntry(new GUIContent("Status Behaviors"), 2);
			tree.Add(statusBehaviorsGroup);
			AddEntries(tree, statusBehaviorTypes, statusBehaviorsGroup);
			AddEntries(tree, leafTypes, leavesGroup);

			return tree;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
		{
			m_graph.CreateNewBehavior((Type)searchTreeEntry.userData, context.screenMousePosition);
			return true;
		}

		private static void AddEntries([NotNull] List<SearchTreeEntry> tree, [NotNull] List<Type> behaviorTypes,
			[NotNull] SearchTreeGroupEntry mainGroupEntry)
		{
			for (int i = 0, count = behaviorTypes.Count; i < count; ++i)
			{
				Type behaviorType = behaviorTypes[i];
				SerializedBehaviorsCollection.TryGetSerializedBehaviorType(behaviorType,
					out Type serializedBehaviorType);
				var groupNameAttribute = serializedBehaviorType.GetCustomAttribute<SearchGroupAttribute>();
				SearchTreeGroupEntry groupEntry = groupNameAttribute == null
					? mainGroupEntry
					: GetOrCreateGroupEntryForPath(tree, mainGroupEntry, groupNameAttribute.groupPath);

				tree.Add(new SearchTreeEntry(new GUIContent(TypeHelper.GetUIName(behaviorType)))
				{
					level = groupEntry.level + 1, userData = serializedBehaviorType
				});
			}
		}

		[NotNull]
		private static SearchTreeGroupEntry GetOrCreateGroupEntryForPath([NotNull] List<SearchTreeEntry> tree,
			[NotNull] SearchTreeGroupEntry baseEntry, [NotNull] string groupPath)
		{
			string[] groups = groupPath.Split('/');

			for (int i = 0, count = groups.Length; i < count; ++i)
			{
				string group = groups[i];
				baseEntry = GetOrAddGroupEntry(tree, baseEntry, group);
			}

			return baseEntry;
		}

		[NotNull]
		private static SearchTreeGroupEntry GetOrAddGroupEntry([NotNull] List<SearchTreeEntry> tree,
			[NotNull] SearchTreeGroupEntry baseEntry, [NotNull] string groupName)
		{
			int baseIndex = tree.IndexOf(baseEntry);
			int finalIndex = baseIndex + 1;
			int level = baseEntry.level + 1;

			for (int count = tree.Count; finalIndex < count; ++finalIndex)
			{
				SearchTreeEntry treeEntry = tree[finalIndex];

				if (treeEntry.level <= baseEntry.level)
				{
					break;
				}

				if (treeEntry is SearchTreeGroupEntry groupEntry &&
					groupEntry.level == level && groupEntry.content.text == groupName)
				{
					return groupEntry;
				}
			}

			int index = baseIndex + 1;

			for (; index < finalIndex; ++index)
			{
				SearchTreeEntry element = tree[index];

				if (element.level == level && string.CompareOrdinal(groupName, element.content.text) < 0)
				{
					break;
				}
			}

			var entry = new SearchTreeGroupEntry(new GUIContent(groupName), level);
			tree.Insert(index, entry);
			return entry;
		}
	}
}
