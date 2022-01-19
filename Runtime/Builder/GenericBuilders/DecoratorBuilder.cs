// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Decorator-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder.GenericBuilders
{
	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator> : DecoratorBuilder
		where TDecorator : Decorator, INotSetupable, new()
	{
		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator>(child);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg">Setup method argument type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg> : DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg>, new()
	{
		/// <summary>
		/// Setup method argument.
		/// </summary>
		private readonly TArg m_arg;

		/// <param name="arg">Setup method argument.</param>
		public DecoratorBuilder(TArg arg)
		{
			m_arg = arg;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg>(child, m_arg);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1> : DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1>(child, m_arg0, m_arg1);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2> : DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;
		/// <summary>
		/// Third argument in a setup method.
		/// </summary>
		private readonly TArg2 m_arg2;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2>(child, m_arg0, m_arg1, m_arg2);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3> : DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;
		/// <summary>
		/// Third argument in a setup method.
		/// </summary>
		private readonly TArg2 m_arg2;
		/// <summary>
		/// Fourth argument in a setup method.
		/// </summary>
		private readonly TArg3 m_arg3;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3>(child,
				m_arg0, m_arg1, m_arg2, m_arg3);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4> : DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;
		/// <summary>
		/// Third argument in a setup method.
		/// </summary>
		private readonly TArg2 m_arg2;
		/// <summary>
		/// Fourth argument in a setup method.
		/// </summary>
		private readonly TArg3 m_arg3;
		/// <summary>
		/// Fifth argument in a setup method.
		/// </summary>
		private readonly TArg4 m_arg4;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(child,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> : DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;
		/// <summary>
		/// Third argument in a setup method.
		/// </summary>
		private readonly TArg2 m_arg2;
		/// <summary>
		/// Fourth argument in a setup method.
		/// </summary>
		private readonly TArg3 m_arg3;
		/// <summary>
		/// Fifth argument in a setup method.
		/// </summary>
		private readonly TArg4 m_arg4;
		/// <summary>
		/// Sixth argument in a setup method.
		/// </summary>
		private readonly TArg5 m_arg5;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(child,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> :
		DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;
		/// <summary>
		/// Third argument in a setup method.
		/// </summary>
		private readonly TArg2 m_arg2;
		/// <summary>
		/// Fourth argument in a setup method.
		/// </summary>
		private readonly TArg3 m_arg3;
		/// <summary>
		/// Fifth argument in a setup method.
		/// </summary>
		private readonly TArg4 m_arg4;
		/// <summary>
		/// Sixth argument in a setup method.
		/// </summary>
		private readonly TArg5 m_arg5;
		/// <summary>
		/// Seventh argument in a setup method.
		/// </summary>
		private readonly TArg6 m_arg6;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
			m_arg6 = arg6;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(child,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6);
		}
	}

	/// <summary>
	/// Generic <see cref="Decorator"/> builder.
	/// </summary>
	/// <typeparam name="TDecorator"><see cref="Decorator"/> built type.</typeparam>
	/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
	/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
	/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
	/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
	/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
	/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		DecoratorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
		/// <summary>
		/// First argument in a setup method.
		/// </summary>
		private readonly TArg0 m_arg0;
		/// <summary>
		/// Second argument in a setup method.
		/// </summary>
		private readonly TArg1 m_arg1;
		/// <summary>
		/// Third argument in a setup method.
		/// </summary>
		private readonly TArg2 m_arg2;
		/// <summary>
		/// Fourth argument in a setup method.
		/// </summary>
		private readonly TArg3 m_arg3;
		/// <summary>
		/// Fifth argument in a setup method.
		/// </summary>
		private readonly TArg4 m_arg4;
		/// <summary>
		/// Sixth argument in a setup method.
		/// </summary>
		private readonly TArg5 m_arg5;
		/// <summary>
		/// Seventh argument in a setup method.
		/// </summary>
		private readonly TArg6 m_arg6;
		/// <summary>
		/// Eighth argument in a setup method.
		/// </summary>
		private readonly TArg7 m_arg7;

		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6,
			TArg7 arg7)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
			m_arg6 = arg6;
			m_arg7 = arg7;
		}

		/// <summary>
		/// <typeparamref name="TDecorator"/> type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(TDecorator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(child,
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6, m_arg7);
		}
	}
}
