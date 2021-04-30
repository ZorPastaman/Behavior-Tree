// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves;
using Zor.BehaviorTree.Serialization.SerializedBehaviors;
using Zor.SimpleBlackboard.Core;
using Object = UnityEngine.Object;

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
		[SerializeField] private NodeGraphInfo m_RootGraphInfo = new NodeGraphInfo
		{
			position = new Vector2(100f, 450f)
		};

		private TreeBuilder m_treeBuilder;

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override TreeRoot CreateTree(Blackboard blackboard)
		{
			if (m_treeBuilder == null)
			{
				m_treeBuilder = new TreeBuilder();
				Deserialize(m_RootNode);
			}

			return m_treeBuilder.Build(blackboard);
		}

		private void Deserialize(int index)
		{
			SerializedBehaviorData data = m_SerializedBehaviorData[index];
			data.serializedBehavior.AddBehavior(m_treeBuilder);

			int[] children = data.childrenIndices;
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				int child = children[i];
				if (child >= 0)
				{
					Deserialize(child);
				}
			}

			m_treeBuilder.Complete();
		}

#if UNITY_EDITOR
		public event Action OnAssetChanged;

		private void OnValidate()
		{
			ValidateNulls();
			ValidateCopies();
			ValidateChildrenCounts();
			ValidateChildren();
			ValidateSubAssets();
			ValidateDependedAssets();
			m_treeBuilder = null;
			OnAssetChanged?.Invoke();
		}

		private void ValidateNulls()
		{
			for (int i = 0; i < m_SerializedBehaviorData.Length; ++i)
			{
				SerializedBehaviorData serializedBehaviorData = m_SerializedBehaviorData[i];
				SerializedBehavior_Base serializedBehavior = serializedBehaviorData.serializedBehavior;

				if (serializedBehavior == null)
				{
					RemoveBehavior(i);
					--i;
				}
			}
		}

		private void ValidateCopies()
		{
			for (int i = 0; i < m_SerializedBehaviorData.Length; ++i)
			{
				SerializedBehaviorData serializedBehaviorData = m_SerializedBehaviorData[i];
				SerializedBehavior_Base serializedBehavior = serializedBehaviorData.serializedBehavior;

				bool copy = false;

				for (int behaviorIndex = 0; behaviorIndex < i & !copy; ++behaviorIndex)
				{
					copy = serializedBehavior == m_SerializedBehaviorData[behaviorIndex].serializedBehavior;
				}

				if (copy)
				{
					RemoveBehavior(i);
					--i;
				}
			}
		}

		private void ValidateChildrenCounts()
		{
			for (int i = 0, count = m_SerializedBehaviorData.Length; i < count; ++i)
			{
				SerializedBehaviorData serializedBehaviorData = m_SerializedBehaviorData[i];
				int[] children = serializedBehaviorData.childrenIndices;
				Type serializedType = serializedBehaviorData.serializedBehavior.serializedBehaviorType;

				if (serializedType.IsSubclassOf(typeof(Leaf)))
				{
					if (children.Length > 0)
					{
						serializedBehaviorData.childrenIndices = new int[0];
					}
				}
				else if (serializedType.IsSubclassOf(typeof(Decorator)))
				{
					if (children.Length > 1)
					{
						int child = children[0];
						serializedBehaviorData.childrenIndices = new[] {child};
					}
					else if (children.Length < 1)
					{
						serializedBehaviorData.childrenIndices = new[] {-1};
					}
				}
				else if (serializedType.IsSubclassOf(typeof(Composite)))
				{
					if (children.Length < 2)
					{
						int[] newChildren = {-1, -1};

						if (children.Length == 1)
						{
							newChildren[0] = children[0];
						}

						serializedBehaviorData.childrenIndices = newChildren;
					}
				}

				m_SerializedBehaviorData[i] = serializedBehaviorData;
			}
		}

		private void ValidateChildren()
		{
			// Self and out of bounds validation
			for (int i = 0, count = m_SerializedBehaviorData.Length; i < count; ++i)
			{
				SerializedBehaviorData serializedBehaviorData = m_SerializedBehaviorData[i];
				int[] children = serializedBehaviorData.childrenIndices;

				for (int childIndex = 0, childCount = children.Length; childIndex < childCount; ++childIndex)
				{
					int child = children[childIndex];

					if (child == i || child >= count)
					{
						children[childIndex] = -1;
					}
				}
			}

			// Multiple links to the same node validation
			for (int i = 0, count = m_SerializedBehaviorData.Length; i < count; ++i)
			{
				int parentIndex = -1;
				int parentPort = -1;

				for (int nodeIndex = 0; nodeIndex < count & parentIndex < 0; ++nodeIndex)
				{
					int[] children = m_SerializedBehaviorData[nodeIndex].childrenIndices;

					for (int childIndex = 0, childCount = children.Length;
						childIndex < childCount & parentIndex < 0;
						++childIndex)
					{
						if (children[childIndex] == i)
						{
							parentIndex = nodeIndex;
							parentPort = childIndex;
						}
					}
				}

				if (parentIndex < 0)
				{
					continue;
				}

				int[] parentChildren = m_SerializedBehaviorData[parentIndex].childrenIndices;

				for (int parentPortIndex = parentPort + 1, parentPortCount = parentChildren.Length;
					parentPortIndex < parentPortCount;
					++parentPortIndex)
				{
					if (parentChildren[parentPortIndex] == i)
					{
						parentChildren[parentPortIndex] = -1;
					}
				}

				for (int nodeIndex = parentIndex + 1; nodeIndex < count; ++nodeIndex)
				{
					int[] children = m_SerializedBehaviorData[nodeIndex].childrenIndices;

					for (int childIndex = 0, childCount = children.Length; childIndex < childCount; ++childIndex)
					{
						if (children[childIndex] == i)
						{
							children[childIndex] = -1;
						}
					}
				}
			}

			// Root out of bounds validation
			if (m_RootNode >= m_SerializedBehaviorData.Length)
			{
				m_RootNode = -1;
			}

			if (m_RootNode < 0)
			{
				return;
			}

			// Multiple links with root validation
			for (int i = 0, count = m_SerializedBehaviorData.Length; i < count; ++i)
			{
				int[] children = m_SerializedBehaviorData[i].childrenIndices;

				for (int childIndex = 0, childCount = children.Length; childIndex < childCount; ++childIndex)
				{
					if (children[childIndex] == m_RootNode)
					{
						children[childIndex] = -1;
					}
				}
			}
		}

		private void ValidateSubAssets()
		{
			string thisAssetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
			Object[] subAssets = UnityEditor.AssetDatabase.LoadAllAssetRepresentationsAtPath(thisAssetPath);

			for (int i = 0, count = subAssets.Length; i < count; ++i)
			{
				Object subAsset = subAssets[i];

				if (subAsset == null)
				{
					continue;
				}

				bool needed = false;

				for (int childIndex = 0, childCount = m_SerializedBehaviorData.Length;
					childIndex < childCount & !needed;
					++childIndex)
				{
					needed = m_SerializedBehaviorData[childIndex].serializedBehavior == subAsset;
				}

				if (!needed)
				{
					UnityEditor.AssetDatabase.RemoveObjectFromAsset(subAsset);
				}
			}
		}

		private void ValidateDependedAssets()
		{
			for (int i = 0; i < m_SerializedBehaviorData.Length; ++i)
			{
				SerializedBehavior_Base serializedBehavior = m_SerializedBehaviorData[i].serializedBehavior;
				string path = UnityEditor.AssetDatabase.GetAssetPath(serializedBehavior);
				Object mainAsset = UnityEditor.AssetDatabase.LoadMainAssetAtPath(path);

				if (mainAsset == null || mainAsset == this)
				{
					continue;
				}

				RemoveBehavior(i);
				--i;
			}
		}

		private void RemoveBehavior(int index)
		{
			int lastIndex = m_SerializedBehaviorData.Length - 1;
			if (index < lastIndex)
			{
				Array.Copy(m_SerializedBehaviorData, index + 1, m_SerializedBehaviorData, index, lastIndex - index);
			}

			Array.Resize(ref m_SerializedBehaviorData, lastIndex);

			for (int behaviorIndex = 0, behaviorCount = m_SerializedBehaviorData.Length;
				behaviorIndex < behaviorCount;
				++behaviorIndex)
			{
				int[] children = m_SerializedBehaviorData[behaviorIndex].childrenIndices;

				for (int childIndex = 0, childCount = children.Length;
					childIndex < childCount;
					++childIndex)
				{
					if (children[childIndex] == index)
					{
						children[childIndex] = -1;
					}
					else if (children[childIndex] > index)
					{
						--children[childIndex];
					}
				}
			}

			if (m_RootNode == index)
			{
				m_RootNode = -1;
			}
			else if (m_RootNode > index)
			{
				--m_RootNode;
			}
		}
#endif
	}
}
