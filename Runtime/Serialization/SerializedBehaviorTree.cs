// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;
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
		[SerializeField] private SerializedBehaviorsData[] m_SerializedBehaviorData;

		private TreeBuilder m_treeBuilder;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override TreeRoot CreateTree(Blackboard blackboard)
		{
			return m_treeBuilder.Build(blackboard);
		}

		private void OnEnable()
		{
			m_treeBuilder = new TreeBuilder();

			if (m_SerializedBehaviorData != null && m_SerializedBehaviorData.Length > 0)
			{
				Deserialize(0);
			}
		}

		private void Deserialize(int index)
		{
			SerializedBehaviorsData data = m_SerializedBehaviorData[index];
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
				Deserialize(children[i]);
			}

			m_treeBuilder.Finish();
		}

#if UNITY_EDITOR
		[ContextMenu("Test")]
		private void Test()
		{
			m_SerializedBehaviorData = new SerializedBehaviorsData[3];

			m_SerializedBehaviorData[0] = new SerializedBehaviorsData
			{
				serializedBehavior = CreateSerializedBehavior<RepeaterSerializedBehavior>(),
				childrenIndices = new[] {1}
			};

			m_SerializedBehaviorData[1] = new SerializedBehaviorsData
			{
				serializedBehavior = CreateSerializedBehavior<InverterSerializedBehavior>(),
				childrenIndices = new[] {2}
			};

			m_SerializedBehaviorData[2] = new SerializedBehaviorsData
			{
				serializedBehavior = CreateSerializedBehavior<SuccessSerializedBehavior>(),
				childrenIndices = new int[0]
			};
		}

		private SerializedBehavior CreateSerializedBehavior<T>() where T : SerializedBehavior
		{
			var serializedBehavior = CreateInstance<T>();
			serializedBehavior.name = typeof(T).Name;
			UnityEditor.AssetDatabase.AddObjectToAsset(serializedBehavior, this);
			return serializedBehavior;
		}
#endif
	}
}
