// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.Core.Leaves
{
	public abstract class Leaf : Behavior
	{
		[NotNull, Pure]
		public static TLeaf Create<TLeaf>() where TLeaf : Leaf, INotSetupable, new()
		{
			return new TLeaf();
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg>(TArg arg) where TLeaf : Leaf, ISetupable<TArg>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1, arg2);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1, arg2, arg3);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1, arg2, arg3, arg4);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1, arg2, arg3, arg4, arg5);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			return leaf;
		}

		[NotNull, Pure]
		public static TLeaf Create<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			var leaf = new TLeaf();
			leaf.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			return leaf;
		}

		[NotNull, Pure]
		public static Leaf Create([NotNull] Type leafType)
		{
			return (Leaf)Activator.CreateInstance(leafType);
		}

		[NotNull, Pure]
		public static Leaf Create([NotNull] Type leafType, [NotNull, ItemCanBeNull] params object[] parameters)
		{
			var leaf = (Leaf)Activator.CreateInstance(leafType);
			CreateSetup(leaf, parameters);
			return leaf;
		}
	}
}
