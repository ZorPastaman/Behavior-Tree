// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Composites;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Actions;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.Conditions;
using Zor.BehaviorTree.Serialization.SerializedBehaviors.Leaves.StatusBehaviors;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors
{
	/// <summary>
	/// Serialized <see cref="Behavior"/>.
	/// </summary>
	/// <typeparam name="T"><see cref="Behavior"/> type.</typeparam>
	/// <remarks>
	/// Don't derive this. You should derive <see cref="SerializedComposite{TComposite}"/>,
	/// <see cref="SerializedDecorator{TDecorator}"/>, <see cref="SerializedLeaf{TLeaf}"/>,
	/// <see cref="SerializedAction{TAction}"/>, <see cref="SerializedCondition{TCondition}"/> or
	/// <see cref="SerializedStatusBehavior{TStatusBehavior}"/>.
	/// </remarks>
	public abstract class SerializedBehavior<T> : SerializedBehavior_Base where T : Behavior
	{
		public sealed override Type serializedBehaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(T);
		}
	}
}
