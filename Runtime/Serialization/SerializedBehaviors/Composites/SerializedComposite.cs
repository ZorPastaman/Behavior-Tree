// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Composites
{
	/// <summary>
	/// Serialized <see cref="Composite"/> with no setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	public abstract class SerializedComposite<TComposite> : SerializedBehavior<TComposite>
		where TComposite : Composite, INotSetupable, new()
	{
		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => null;
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite>();
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg">Argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg> : SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg>, new()
	{
		[SerializeField] private TArg m_Arg0;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg>(m_Arg0);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1> : SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1>(m_Arg0, m_Arg1);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1, TArg2> : SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;
		[SerializeField] private TArg2 m_Arg2;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1, m_Arg2};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1, TArg2>(m_Arg0, m_Arg1, m_Arg2);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1, TArg2, TArg3> : SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;
		[SerializeField] private TArg2 m_Arg2;
		[SerializeField] private TArg3 m_Arg3;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1, m_Arg2, m_Arg3};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3>(m_Arg0, m_Arg1, m_Arg2, m_Arg3);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4> :
		SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;
		[SerializeField] private TArg2 m_Arg2;
		[SerializeField] private TArg3 m_Arg3;
		[SerializeField] private TArg4 m_Arg4;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> :
		SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;
		[SerializeField] private TArg2 m_Arg2;
		[SerializeField] private TArg3 m_Arg3;
		[SerializeField] private TArg4 m_Arg4;
		[SerializeField] private TArg5 m_Arg5;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> :
		SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;
		[SerializeField] private TArg2 m_Arg2;
		[SerializeField] private TArg3 m_Arg3;
		[SerializeField] private TArg4 m_Arg4;
		[SerializeField] private TArg5 m_Arg5;
		[SerializeField] private TArg6 m_Arg6;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5, m_Arg6};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5, m_Arg6);
		}
	}

	/// <summary>
	/// Serialized <see cref="Composite"/> with a setup method.
	/// </summary>
	/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
	/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
	/// <remarks>
	/// Use <see cref="Zor.BehaviorTree.DrawingAttributes.NameOverrideAttribute"/> to give the serialized arguments
	/// custom names. The index in the attribute matches the index of the argument.
	/// </remarks>
	public abstract class SerializedComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		SerializedBehavior<TComposite>
		where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
		[SerializeField] private TArg0 m_Arg0;
		[SerializeField] private TArg1 m_Arg1;
		[SerializeField] private TArg2 m_Arg2;
		[SerializeField] private TArg3 m_Arg3;
		[SerializeField] private TArg4 m_Arg4;
		[SerializeField] private TArg5 m_Arg5;
		[SerializeField] private TArg6 m_Arg6;
		[SerializeField] private TArg7 m_Arg7;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5, m_Arg6, m_Arg7};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5, m_Arg6, m_Arg7);
		}
	}
}
