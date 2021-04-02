// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using UnityEngine;

namespace Zor.BehaviorTree.Serialization
{
	[Serializable]
	public struct BehaviorSerializedData
	{
		[SerializeField] public string TypeName;
		[SerializeReference] public object[] CustomData;
		[SerializeField] public int[] ChildrenIndices;
	}
}
