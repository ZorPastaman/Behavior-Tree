// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public abstract class Decorator : Behavior
	{
		[NotNull] protected Behavior child;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal sealed override void Initialize()
		{
			base.Initialize();
			child.Initialize();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal sealed override void Dispose()
		{
			child.Dispose();
			base.Dispose();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal sealed override void SetBlackboard(Blackboard blackboardToSet)
		{
			base.SetBlackboard(blackboardToSet);
			child.SetBlackboard(blackboardToSet);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected private sealed override void OnAbortInternal()
		{
			child.Abort();
			base.OnAbortInternal();
		}

		public static TDecorator Create<TDecorator>([NotNull] Behavior child)
			where TDecorator : Decorator, INotSetupable, new()
		{
			var decorator = new TDecorator {child = child};
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg>([NotNull] Behavior child, TArg arg)
			where TDecorator : Decorator, ISetupable<TArg>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1>([NotNull] Behavior child, TArg0 arg0, TArg1 arg1)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1, arg2);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1, arg2, arg3);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1, arg2, arg3, arg4);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1, arg2, arg3, arg4, arg5);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			[NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			decorator.child = child;
			return decorator;
		}

		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			[NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			var decorator = new TDecorator();
			decorator.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			decorator.child = child;
			return decorator;
		}

		public static Decorator Create([NotNull] Type decoratorType, [NotNull] Behavior child)
		{
			var decorator = (Decorator)Activator.CreateInstance(decoratorType);
			decorator.child = child;
			return decorator;
		}

		public static Decorator Create([NotNull] Type decoratorType, [NotNull] Behavior child,
			[NotNull] params object[] parameters)
		{
			var decorator = (Decorator)Activator.CreateInstance(decoratorType);
			CreateSetup(decorator, parameters);
			decorator.child = child;
			return decorator;
		}
	}
}
