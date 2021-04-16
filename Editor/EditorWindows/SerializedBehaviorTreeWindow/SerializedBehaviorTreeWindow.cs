// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Serialization;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SerializedBehaviorTreeWindow : EditorWindow
	{
		private SerializedBehaviorTreeGraph m_graph;

		[MenuItem("Window/Behavior Tree/Serialized Behavior Tree", priority = 2021)]
		public static void OpenWindow()
		{
			GetWindow<SerializedBehaviorTreeWindow>("Serialized Behavior Tree");
		}

		[OnOpenAsset(0)]
		public static bool OnOpenAsset(int instanceID, int line)
		{
			string path = AssetDatabase.GetAssetPath(instanceID);

			if (AssetDatabase.GetMainAssetTypeAtPath(path) != typeof(SerializedBehaviorTree))
			{
				return false;
			}

			OpenWindow();

			return true;
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
			m_graph?.Dispose();
			m_graph = null;

			rootVisualElement.Clear();

			var serializedBehaviorTree = Selection.activeObject as SerializedBehaviorTree;
			if (serializedBehaviorTree == null)
			{
				return;
			}

			m_graph = new SerializedBehaviorTreeGraph(serializedBehaviorTree, this)
			{
				name = "Serialized Behavior Tree"
			};
			m_graph.StretchToParentSize();
			rootVisualElement.Add(m_graph);
		}
	}
}
