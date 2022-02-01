// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors
{
	/// <summary>
	/// Base for <see cref="SerializedBehavior{T}"/>. It's needed because of Unity serialization.
	/// </summary>
	public abstract class SerializedBehavior_Base : ScriptableObject
	{
		/// <summary>
		/// Type of a serialized <see cref="Zor.BehaviorTree.Core.Behavior"/>.
		/// </summary>
		[NotNull]
		public abstract Type serializedBehaviorType { get; }

		/// <summary>
		/// Serialized custom data. It's used in setup methods.
		/// </summary>
		[CanBeNull]
		public virtual object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => null;
		}

		/// <summary>
		/// Gets serialized data.
		/// </summary>
		/// <returns><see cref="serializedBehaviorType"/> and <see cref="serializedCustomData"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public (Type, object[]) GetSerializedData()
		{
			return (serializedBehaviorType, serializedCustomData);
		}

		/// <summary>
		/// Adds its <see cref="Zor.BehaviorTree.Core.Behavior"/> into the <paramref name="treeBuilder"/>.
		/// </summary>
		/// <param name="treeBuilder">Used <see cref="TreeBuilder"/>.</param>
		/// <remarks>
		/// This method doesn't call <see cref="TreeBuilder.Complete"/>.
		/// </remarks>
		public abstract void AddBehavior([NotNull] TreeBuilder treeBuilder);
	}
}
