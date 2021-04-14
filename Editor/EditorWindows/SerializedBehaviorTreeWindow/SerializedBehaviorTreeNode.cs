// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Helpers;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;

namespace Zor.BehaviorTree.EditorWindows.SerializedBehaviorTreeWindow
{
	public sealed class SerializedBehaviorTreeNode : Node
	{
		[NotNull] private readonly SerializedBehavior_Base m_dependedSerializedBehavior;

		public SerializedBehaviorTreeNode([NotNull] SerializedBehavior_Base dependedSerializedBehavior)
		{
			m_dependedSerializedBehavior = dependedSerializedBehavior;

			Type type = m_dependedSerializedBehavior.serializedType;

			title = type.Name;

			Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
				typeof(BehaviorConnection));
			input.portName = "Parent";
			inputContainer.Add(input);

			if (type.IsSubclassOf(typeof(Composite)))
			{
				var addButton = new Button {text = "+"};
				titleButtonContainer.Add(addButton);
			}

			UIElementsHelper.CreatePropertyElements(new SerializedObject(m_dependedSerializedBehavior),
				extensionContainer);
		}

		[NotNull]
		public SerializedBehavior_Base dependedSerializedBehavior => m_dependedSerializedBehavior;

		public void SetOutputCapacity(int capacity)
		{
			if (outputContainer.childCount > capacity)
			{
				for (int i = 0, count = outputContainer.childCount - capacity; i < count; ++i)
				{
					outputContainer.RemoveAt(outputContainer.childCount - 1);
				}
			}
			else if (outputContainer.childCount < capacity)
			{
				for (int i = 0, count = capacity - outputContainer.childCount; i < count; ++i)
				{
					AddOutputPort();
				}
			}
		}

		[NotNull]
		public Edge SetChild([NotNull] SerializedBehaviorTreeNode child, int index)
		{
			var outputPort = (Port)outputContainer[index];
			var inputPort = (Port)child.inputContainer[0];
			return outputPort.ConnectTo(inputPort);
		}

		private void AddOutputPort()
		{
			Port childPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(BehaviorConnection));
			childPort.portName = "Child";
			outputContainer.Add(childPort);
		}
	}
}
