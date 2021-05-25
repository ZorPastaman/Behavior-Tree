// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.BehaviorTree.Helpers;

namespace Zor.BehaviorTree.EditorWindows.AgentBehaviorTreeWindow
{
	public sealed class AgentBehaviorTreeNode : Node
	{
		[NotNull] private readonly Behavior m_behavior;
		[NotNull] private readonly FieldInfo[] m_infoFields;
		[NotNull] private readonly Label[] m_infoViews;

		public AgentBehaviorTreeNode([NotNull] Behavior behavior)
		{
			m_behavior = behavior;
			Type behaviorType = behavior.GetType();

			title = TypeHelper.GetUIName(behaviorType);

			Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
				typeof(BehaviorConnection));
			input.portName = "Parent";
			inputContainer.Add(input);

			m_infoFields = GetBehaviorInfos(behaviorType);
			int infoCount = m_infoFields.Length;
			m_infoViews = new Label[infoCount];

			for (int i = 0; i < infoCount; ++i)
			{
				var root = new VisualElement();
				IStyle rootStyle = root.style;
				rootStyle.flexDirection = FlexDirection.Row;
				rootStyle.justifyContent = Justify.SpaceBetween;
				rootStyle.marginLeft = 5f;
				rootStyle.marginRight = 5f;
				extensionContainer.Add(root);

				FieldInfo infoField = m_infoFields[i];
				var label = new Label(NameHelper.GetNameForField(infoField.Name));
				IStyle labelStyle = label.style;
				labelStyle.flexGrow = 0f;
				labelStyle.borderRightWidth = 10f;
				root.Add(label);

				object value = infoField.GetValue(m_behavior);
				string valueString = value == null ? "null" : value.ToString();
				Label infoView = m_infoViews[i] = new Label(valueString);
				infoView.style.flexGrow = 1f;
				infoView.style.unityTextAlign = TextAnchor.MiddleRight;
				root.Add(infoView);
			}
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		{
		}

		public Edge AddChild([NotNull] AgentBehaviorTreeNode child)
		{
			Port output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
				typeof(SerializedBehaviorTreeWindow.BehaviorConnection));
			output.portName = "Child";
			outputContainer.Add(output);

			return output.ConnectTo((Port)child.inputContainer[0]);
		}

		public void Update()
		{
			Color statusColor;

			switch (m_behavior.status)
			{
				case Status.Idle:
					statusColor = Color.black;
					break;
				case Status.Success:
					statusColor = Color.green;
					break;
				case Status.Running:
					statusColor = Color.yellow;
					break;
				case Status.Failure:
					statusColor = Color.red;
					break;
				case Status.Error:
					statusColor = Color.cyan;
					break;
				case Status.Abort:
					statusColor = Color.white;
					break;
				default:
					statusColor = Color.magenta;
					break;
			}

			IStyle mainStyle = mainContainer.style;
			mainStyle.borderTopColor = mainStyle.borderBottomColor =
				mainStyle.borderLeftColor = mainStyle.borderRightColor = statusColor;
			statusColor.a = elementTypeColor.a;
			elementTypeColor = statusColor;

			for (int i = 0, count = m_infoFields.Length; i < count; ++i)
			{
				object value = m_infoFields[i].GetValue(m_behavior);
				string valueString = value == null ? "null" : value.ToString();
				m_infoViews[i].text = valueString;
			}
		}

		private static FieldInfo[] GetBehaviorInfos([NotNull] Type behaviorType)
		{
			var behaviorInfos = new List<FieldInfo>();

			do
			{
				IEnumerable<FieldInfo> fields = behaviorType
					.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					.Where(field => field.GetCustomAttribute<BehaviorInfoAttribute>() != null);
				behaviorInfos.AddRange(fields);
				behaviorType = behaviorType.BaseType;
			} while (behaviorType != null);

			return behaviorInfos.ToArray();
		}
	}
}
