// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using UnityEngine.Profiling;

namespace Zor.BehaviorTree.Core.Leaves
{
	public abstract class Leaf : Behavior
	{
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
