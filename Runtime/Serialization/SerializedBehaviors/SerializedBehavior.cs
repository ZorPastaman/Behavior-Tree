// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors
{
	/// <summary>
	/// Serialized <see cref="Behavior"/>.
	/// </summary>
	/// <typeparam name="T"><see cref="Behavior"/> type.</typeparam>
	public abstract class SerializedBehavior<T> : SerializedBehavior_Base where T : Behavior
	{
		public sealed override Type serializedBehaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(T);
		}
	}
}
