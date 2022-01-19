// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	/// <summary>
	/// Decorator behavior. It has a child behavior.
	/// </summary>
	/// <remarks>
	/// Inheritors must have a default constructor only.
	/// They must use <see cref="ISetupable{TArg}"/> or other setup interfaces to get a constructor functionality.
	/// </remarks>
	public abstract class Decorator : Behavior
	{
		/// <summary>
		/// Child behavior.
		/// </summary>
		protected Behavior child;

		/// <summary>
		/// Initializes itself and the child.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal sealed override void Initialize()
		{
			base.Initialize();
			child.Initialize();
		}

		/// <summary>
		/// Disposes the child and itself.
		/// </summary>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg">Setup method argument.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg">Setup method argument type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior.
		/// </summary>
		/// <param name="child">Child behavior.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		/// <typeparam name="TDecorator">Decorator type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior without a setup method.
		/// </summary>
		/// <param name="decoratorType">Decorator type. Must be derived from <see cref="Decorator"/>.</param>
		/// <param name="child">Child behavior.</param>
		/// <returns>Created decorator.</returns>
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

		/// <summary>
		/// Creates a decorator behavior with a setup method.
		/// </summary>
		/// <param name="decoratorType">Decorator type. Must be derived from <see cref="Decorator"/>.</param>
		/// <param name="child">Child behavior.</param>
		/// <param name="parameters">Setup method arguments.</param>
		/// <returns>Created decorator.</returns>
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
