// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	/// <summary>
	/// Composite behavior. It has children behaviors.
	/// </summary>
	/// <remarks>
	/// Inheritors must have a default constructor only.
	/// They must use <see cref="ISetupable{TArg}"/> or other setup interfaces to get a constructor functionality.
	/// </remarks>
	public abstract class Composite : Behavior
	{
		/// <summary>
		/// Children behaviors.
		/// </summary>
		protected Behavior[] children;

		/// <summary>
		/// Initializes itself and all the children.
		/// </summary>
		internal sealed override void Initialize()
		{
			base.Initialize();

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Initialize();
			}
		}

		/// <summary>
		/// Disposes all the children and itself.
		/// </summary>
		internal sealed override void Dispose()
		{
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Dispose();
			}

			base.Dispose();
		}

		internal sealed override void SetBlackboard(Blackboard blackboardToSet)
		{
			base.SetBlackboard(blackboardToSet);

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].SetBlackboard(blackboardToSet);
			}
		}

		protected private sealed override void OnAbortInternal()
		{
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Abort();
			}

			base.OnAbortInternal();
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite>([NotNull, ItemNotNull] Behavior[] children)
			where TComposite : Composite, INotSetupable, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();
			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg">Setup method argument.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg">Setup method argument type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg>([NotNull, ItemNotNull] Behavior[] children, TArg arg)
			where TComposite : Composite, ISetupable<TArg>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First method argument.</param>
		/// <param name="arg1">Second method argument.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">Fist method argument type.</typeparam>
		/// <typeparam name="TArg1">Second method argument type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1>([NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1)
			where TComposite : Composite, ISetupable<TArg0, TArg1>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2>([NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1, arg2);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1, arg2, arg3);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1, arg2, arg3, arg4);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1, arg2, arg3, arg4, arg5);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior.
		/// </summary>
		/// <param name="children">Children behaviors.</param>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		/// <typeparam name="TComposite">Composite type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(typeof(TComposite).FullName);

			var composite = new TComposite();

			Profiler.BeginSample("Setup");
			composite.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			Profiler.EndSample();

			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior without a setup method..
		/// </summary>
		/// <param name="compositeType">Composite type. Must be derived from <see cref="Composite"/>.</param>
		/// <param name="children">Children behaviors.</param>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static Composite Create([NotNull] Type compositeType, [NotNull, ItemNotNull] Behavior[] children)
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(compositeType.FullName);

			var composite = (Composite)Activator.CreateInstance(compositeType);
			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		/// <summary>
		/// Creates a composite behavior with a setup method.
		/// </summary>
		/// <param name="compositeType">Composite type. Must be derived from <see cref="Composite"/>.</param>
		/// <param name="children">Children behaviors.</param>
		/// <param name="parameters">Setup method arguments.</param>
		/// <returns>Created composite.</returns>
		[NotNull, Pure]
		public static Composite Create([NotNull] Type compositeType, [NotNull, ItemNotNull] Behavior[] children,
			[NotNull, ItemCanBeNull] params object[] parameters)
		{
			Profiler.BeginSample("Composite.Create");
			Profiler.BeginSample(compositeType.FullName);

			var composite = (Composite)Activator.CreateInstance(compositeType);
			CreateSetup(composite, parameters);
			SetChildren(composite, children);

			Profiler.EndSample();
			Profiler.EndSample();

			return composite;
		}

		private static void SetChildren([NotNull] Composite composite, [NotNull] Behavior[] children)
		{
			int childCount = children.Length;
			composite.children = new Behavior[childCount];
			Array.Copy(children, 0, composite.children, 0, childCount);
		}
	}
}
