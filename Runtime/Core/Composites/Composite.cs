// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	public abstract class Composite : Behavior
	{
		protected Behavior[] children;

		internal sealed override void Initialize()
		{
			base.Initialize();

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Initialize();
			}
		}

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

		[NotNull, Pure]
		public static TComposite Create<TComposite>([NotNull, ItemNotNull] Behavior[] children)
			where TComposite : Composite, INotSetupable, new()
		{
			var composite = new TComposite();
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg>([NotNull, ItemNotNull] Behavior[] children, TArg arg)
			where TComposite : Composite, ISetupable<TArg>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1>([NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1)
			where TComposite : Composite, ISetupable<TArg0, TArg1>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2>([NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1, arg2);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1, arg2, arg3);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1, arg2, arg3, arg4);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1, arg2, arg3, arg4, arg5);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static TComposite Create<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			[NotNull, ItemNotNull] Behavior[] children,
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			var composite = new TComposite();
			composite.Setup(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static Composite Create([NotNull] Type compositeType, [NotNull, ItemNotNull] Behavior[] children)
		{
			var composite = (Composite)Activator.CreateInstance(compositeType);
			SetChildren(composite, children);
			return composite;
		}

		[NotNull, Pure]
		public static Composite Create([NotNull] Type compositeType, [NotNull, ItemNotNull] Behavior[] children,
			[NotNull] params object[] parameters)
		{
			var composite = (Composite)Activator.CreateInstance(compositeType);
			CreateSetup(composite, parameters);
			SetChildren(composite, children);
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
