// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors
{
	public abstract class SerializedBehavior_Base : ScriptableObject
	{
		[NotNull]
		public abstract Type serializedType { get; }

		[CanBeNull]
		public virtual object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => null;
		}

		[Pure]
		public (Type, object[]) GetSerializedData()
		{
			return (serializedType, serializedCustomData);
		}
	}
}
