// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEditor;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Serialization;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SerializedBehaviorTreeWindow : EditorWindow
	{
		[MenuItem("Window/Behavior Tree/Serialized Behavior Tree", priority = 2021)]
		public static void OpenWindow()
		{
			GetWindow<SerializedBehaviorTreeWindow>("Serialized Behavior Tree");
		}

		private void CreateGUI()
		{
			RecreateGraph();
		}

		private void OnSelectionChange()
		{
			RecreateGraph();
		}

		private void RecreateGraph()
		{
			rootVisualElement.Clear();

			var serializedBehaviorTree = Selection.activeObject as SerializedBehaviorTree;
			if (serializedBehaviorTree == null)
			{
				return;
			}

			var graph = new SerializedBehaviorTreeGraph(serializedBehaviorTree) {name = "Serialized Behavior Tree"};
			graph.StretchToParentSize();
			rootVisualElement.Add(graph);
		}
	}
}
