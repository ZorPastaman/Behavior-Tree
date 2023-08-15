// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine.Profiling;

namespace Zor.BehaviorTree.Core.Leaves
{
	/// <summary>
	/// Leaf behavior. It doesn't have a child behavior.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Inheritors must have a default constructor only.
	/// They must use <see cref="ISetupable{TArg}"/> or other setup interfaces to get a constructor functionality.
	/// </para>
	/// <para>
	/// It's recommended to derive <see cref="Zor.BehaviorTree.Core.Leaves.Actions.Action"/> or
	/// <see cref="Zor.BehaviorTree.Core.Leaves.Conditions.Condition"/>.
	/// </para>
	/// </remarks>
	public abstract class Leaf : Behavior
	{
		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf>() where TLeaf : Leaf, INotSetupable, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg">Setup method argument.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg">Setup method argument type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg>(TArg arg) where TLeaf : Leaf, ISetupable<TArg>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1, arg2);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1, arg2, arg3);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1, arg2, arg3, arg4);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1, arg2, arg3, arg4, arg5);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		/// <typeparam name="TLeaf">Leaf type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			var leaf = new TLeaf();

			Profiler.BeginSample("Setup");
			leaf.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			Profiler.EndSample();

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="leafType">Leaf type. Must be derived from <see cref="Leaf"/>.</param>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static Leaf Create([NotNull] Type leafType)
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(leafType.FullName);

			var leaf = (Leaf)Activator.CreateInstance(leafType);

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}

		/// <summary>
		/// Creates a leaf behavior.
		/// </summary>
		/// <param name="leafType">Leaf type. Must be derived from <see cref="Leaf"/>.</param>
		/// <param name="parameters">Setup method arguments.</param>
		/// <returns>Created leaf.</returns>
		[NotNull, Pure]
		public static Leaf Create([NotNull] Type leafType, [NotNull, ItemCanBeNull] params object[] parameters)
		{
			Profiler.BeginSample("Leaf.Create");
			Profiler.BeginSample(leafType.FullName);

			var leaf = (Leaf)Activator.CreateInstance(leafType);
			CreateSetup(leaf, parameters);

			Profiler.EndSample();
			Profiler.EndSample();

			return leaf;
		}
	}
}
