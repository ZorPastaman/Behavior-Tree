// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.Builder;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Serialization.SerializedBehaviors.Decorators
{
	public abstract class SerializedDecorator<TDecorator> : SerializedBehavior<TDecorator>
		where TDecorator : Decorator, INotSetupable, new()
	{
		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => null;
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddDecorator<TDecorator>();
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg> : SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg>, new()
	{
		[SerializeField] private TArg m_Arg0;

		public sealed override object[] serializedCustomData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => new object[] {m_Arg0};
		}

		public sealed override void AddBehavior(TreeBuilder treeBuilder)
		{
			treeBuilder.AddDecorator<TDecorator, TArg>(m_Arg0);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1> : SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1>(m_Arg0, m_Arg1);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1, TArg2> : SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1, TArg2>(m_Arg0, m_Arg1, m_Arg2);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3> : SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3>(m_Arg0, m_Arg1, m_Arg2, m_Arg3);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4> :
		SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> :
		SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> :
		SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5, m_Arg6);
		}
	}

	public abstract class SerializedDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		SerializedBehavior<TDecorator>
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
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
			treeBuilder.AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				m_Arg0, m_Arg1, m_Arg2, m_Arg3, m_Arg4, m_Arg5, m_Arg6, m_Arg7);
		}
	}
}
