// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public abstract class Decorator : Behavior
	{
		protected Behavior child;

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

		[NotNull, Pure]
		public static TDecorator Create<TDecorator>([NotNull] Behavior child)
			where TDecorator : Decorator, INotSetupable, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator {child = child};

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg>([NotNull] Behavior child, TArg arg)
			where TDecorator : Decorator, ISetupable<TArg>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1>([NotNull] Behavior child, TArg0 arg0, TArg1 arg1)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1, arg2);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1, arg2, arg3);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1, arg2, arg3, arg4);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>([NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1, arg2, arg3, arg4, arg5);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			[NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static TDecorator Create<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			[NotNull] Behavior child,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			var decorator = new TDecorator();

			Profiler.BeginSample("Setup");
			decorator.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			Profiler.EndSample();

			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static Decorator Create([NotNull] Type decoratorType, [NotNull] Behavior child)
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(decoratorType.FullName);

			var decorator = (Decorator)Activator.CreateInstance(decoratorType);
			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}

		[NotNull, Pure]
		public static Decorator Create([NotNull] Type decoratorType, [NotNull] Behavior child,
			[NotNull, ItemCanBeNull] params object[] parameters)
		{
			Profiler.BeginSample("Decorator.Create");
			Profiler.BeginSample(decoratorType.FullName);

			var decorator = (Decorator)Activator.CreateInstance(decoratorType);
			CreateSetup(decorator, parameters);
			decorator.child = child;

			Profiler.EndSample();
			Profiler.EndSample();

			return decorator;
		}
	}
}
