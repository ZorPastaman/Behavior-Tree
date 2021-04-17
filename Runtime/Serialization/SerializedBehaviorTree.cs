// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Composite;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.StatusBehaviors;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Serialization
{
	[CreateAssetMenu(
		menuName = "Behavior Tree/Serialized Behavior Tree",
		fileName = "SerializedBehaviorTree",
		order = 448
	)]
	public sealed class SerializedBehaviorTree : SerializedBehaviorTree_Base
	{
		[SerializeField] private SerializedBehaviorData[] m_SerializedBehaviorData;
		[SerializeField] private int m_RootNode = -1;
		[SerializeField] private NodeGraphInfo m_RootGraphInfo;

		private TreeBuilder m_treeBuilder;

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override TreeRoot CreateTree(Blackboard blackboard)
		{
			return m_treeBuilder.Build(blackboard);
		}

		private void OnEnable()
		{
			m_treeBuilder = new TreeBuilder();

			if (m_RootNode >= 0)
			{
				Deserialize(m_RootNode);
			}
		}

		private void Deserialize(int index)
		{
			SerializedBehaviorData data = m_SerializedBehaviorData[index];
			(Type type, object[] customData) = data.serializedBehavior.GetSerializedData();

			if (customData != null)
			{
				m_treeBuilder.AddBehavior(type, customData);
			}
			else
			{
				m_treeBuilder.AddBehavior(type);
			}

			int[] children = data.childrenIndices;

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				int child = children[i];

				if (child < 0)
				{
					continue;
				}

				Deserialize(child);
			}

			m_treeBuilder.Complete();
		}

#if UNITY_EDITOR
		[ContextMenu("Test")]
		private void Test()
		{
			m_SerializedBehaviorData = new SerializedBehaviorData[5];

			m_SerializedBehaviorData[0] = new SerializedBehaviorData
			{
				serializedBehavior = CreateSerializedBehavior<RepeaterSerializedBehavior>(),
				childrenIndices = new[] {3}
			};

			m_SerializedBehaviorData[1] = new SerializedBehaviorData
			{
				serializedBehavior = CreateSerializedBehavior<InverterSerializedBehavior>(),
				childrenIndices = new[] {2}
			};

			m_SerializedBehaviorData[2] = new SerializedBehaviorData
			{
				serializedBehavior = CreateSerializedBehavior<SuccessSerializedBehavior>(),
				childrenIndices = new int[0]
			};

			m_SerializedBehaviorData[3] = new SerializedBehaviorData
			{
				serializedBehavior = CreateSerializedBehavior<SuccessSerializedBehavior>(),
				childrenIndices = new int[0]
			};

			m_SerializedBehaviorData[4] = new SerializedBehaviorData
			{
				serializedBehavior = CreateSerializedBehavior<SelectorSerializedBehavior>(),
				childrenIndices = new[] {0, 1}
			};

			m_RootNode = 4;
		}

		private SerializedBehavior_Base CreateSerializedBehavior<T>() where T : SerializedBehavior_Base
		{
			var serializedBehavior = CreateInstance<T>();
			serializedBehavior.name = typeof(T).Name;
			UnityEditor.AssetDatabase.AddObjectToAsset(serializedBehavior, this);
			return serializedBehavior;
		}
#endif
	}
}
