// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.StatusBehaviors;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Serialization
{
	[CreateAssetMenu(
		menuName = "Behavior Tree/Serialized Behavior Tree",
		fileName = "SerializedBehaviorTree",
		order = 448
	)]
	public sealed class SerializedBehaviorTree : ScriptableObject
	{
		[SerializeField] private BehaviorSerializedData[] m_BehaviorSerializedData;

		private TreeBuilder m_treeBuilder;

		public TreeRoot CreateTree()
		{
			return CreateTree(new Blackboard());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeRoot CreateTree([NotNull] Blackboard blackboard)
		{
			return m_treeBuilder.Build(blackboard);
		}

		private void OnEnable()
		{
			m_treeBuilder = new TreeBuilder();

			if (m_BehaviorSerializedData != null && m_BehaviorSerializedData.Length > 0)
			{
				Deserialize(0);
			}
		}

		private void Deserialize(int index)
		{
			BehaviorSerializedData data = m_BehaviorSerializedData[index];

			var type = Type.GetType(data.TypeName);
			if (type == null)
			{
				return;
			}

			object[] customData = data.CustomData;

			if (customData == null)
			{
				m_treeBuilder.AddBehavior(type);
			}
			else
			{
				m_treeBuilder.AddBehavior(type, customData);
			}

			int[] childrenIndices = data.ChildrenIndices;
			if (childrenIndices != null)
			{
				for (int i = 0, count = childrenIndices.Length; i < count; ++i)
				{
					Deserialize(childrenIndices[i]);
				}
			}

			m_treeBuilder.Finish();
		}

		[ContextMenu("Test")]
		private void Test()
		{
			m_BehaviorSerializedData = new BehaviorSerializedData[3];

			m_BehaviorSerializedData[0] = new BehaviorSerializedData
			{
				TypeName = typeof(Repeater).AssemblyQualifiedName,
				CustomData = new object[] {3u},
				ChildrenIndices = new[] {1}
			};
			m_BehaviorSerializedData[1] = new BehaviorSerializedData
			{
				TypeName = typeof(Inverter).AssemblyQualifiedName,
				CustomData = null,
				ChildrenIndices = new[] {2}
			};
			m_BehaviorSerializedData[2] = new BehaviorSerializedData
			{
				TypeName = typeof(SuccessBehavior).AssemblyQualifiedName,
				CustomData = null,
				ChildrenIndices = null
			};

			OnEnable();
		}
	}
}
