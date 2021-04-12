// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Zor.BehaviorTree.Helpers
{
	internal static class UIElementsHelper
	{
		public static void CreatePropertyElements([NotNull] SerializedObject serializedObject,
			[NotNull] VisualElement root)
		{
			SerializedProperty iterator = serializedObject.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					if (iterator.name == "m_Script")
					{
						continue;
					}

					var propertyField = new PropertyField(iterator) {name = iterator.propertyPath};
					propertyField.Bind(serializedObject);
					root.Add(propertyField);
				} while (iterator.NextVisible(false));
			}
		}
	}
}
