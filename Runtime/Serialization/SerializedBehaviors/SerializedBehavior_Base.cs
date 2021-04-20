// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors
{
	public abstract class SerializedBehavior_Base : ScriptableObject
	{
		[NotNull]
		public abstract Type serializedBehaviorType { get; }

		[CanBeNull]
		public virtual object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public (Type, object[]) GetSerializedData()
		{
			return (serializedBehaviorType, serializedCustomData);
		}

		public abstract void AddBehavior([NotNull] TreeBuilder treeBuilder);
	}
}
