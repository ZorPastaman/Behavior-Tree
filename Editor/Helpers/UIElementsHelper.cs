// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Helpers
{
	internal static class UIElementsHelper
	{
		public static void CreatePropertyElements([NotNull] SerializedObject serializedObject,
			[NotNull] VisualElement root)
		{
			SerializedProperty iterator = serializedObject.GetIterator();
			NameOverrideAttribute[] nameOverrides = serializedObject.targetObject.GetType()
				.GetCustomAttributes<NameOverrideAttribute>().ToArray();

			int propertyIndex = 0;

			if (iterator.NextVisible(true))
			{
				do
				{
					if (iterator.name == "m_Script")
					{
						continue;
					}

					NameOverrideAttribute nameOverride = null;

					for (int i = 0, count = nameOverrides.Length; i < count; ++i)
					{
						if (nameOverrides[i].index == propertyIndex)
						{
							nameOverride = nameOverrides[i];
							break;
						}
					}

					// Dirty hack 'cause an enum field equals -1 at the frame when its object is created.
					if (iterator.propertyType == SerializedPropertyType.Enum &&
						(iterator.enumValueIndex < 0 || iterator.enumValueIndex >= iterator.enumDisplayNames.Length))
					{
						iterator.enumValueIndex = 0;
						serializedObject.ApplyModifiedProperties();
					}

					string label = nameOverride == null ? string.Empty : nameOverride.name;
					var propertyField = new PropertyField(iterator, label) {name = iterator.propertyPath};
					propertyField.Bind(serializedObject);
					root.Add(propertyField);
					++propertyIndex;
				} while (iterator.NextVisible(false));
			}
		}
	}
}
