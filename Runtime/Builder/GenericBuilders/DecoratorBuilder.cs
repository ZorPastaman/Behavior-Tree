﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Decorator-Tree

using System.Runtime.CompilerServices;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder.GenericBuilders
{
	internal sealed class DecoratorBuilder<TDecorator> : IBehaviorBuilder
		where TDecorator : Decorator, INotSetupable, new()
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator>(children[0]);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg>, new()
	{
		private readonly TArg m_arg;

		public DecoratorBuilder(TArg arg)
		{
			m_arg = arg;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg>(children[0], m_arg);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;

		public DecoratorBuilder(TArg0 arg0, TArg1 arg1)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1>(children[0], m_arg0, m_arg1);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;

		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2>(children[0], m_arg0, m_arg1, m_arg2);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;

		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3>(children[0],
				m_arg0, m_arg1, m_arg2, m_arg3);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;

		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(children[0],
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;

		public DecoratorBuilder(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
		{
			m_arg0 = arg0;
			m_arg1 = arg1;
			m_arg2 = arg2;
			m_arg3 = arg3;
			m_arg4 = arg4;
			m_arg5 = arg5;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(children[0],
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;
		private readonly TArg6 m_arg6;

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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(children[0],
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6);
		}
	}

	internal sealed class DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> :
		IBehaviorBuilder
		where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
	{
		private readonly TArg0 m_arg0;
		private readonly TArg1 m_arg1;
		private readonly TArg2 m_arg2;
		private readonly TArg3 m_arg3;
		private readonly TArg4 m_arg4;
		private readonly TArg5 m_arg5;
		private readonly TArg6 m_arg6;
		private readonly TArg7 m_arg7;

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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(children[0],
				m_arg0, m_arg1, m_arg2, m_arg3, m_arg4, m_arg5, m_arg6, m_arg7);
		}
	}
}
