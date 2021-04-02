// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	public abstract class Composite : Behavior
	{
		[NotNull] protected Behavior[] children;

		public static T Create<T>([NotNull] Behavior[] children) where T : Composite, new()
		{
			return new T {children = children};
		}

		public static Composite Create([NotNull] Type compositeType, [NotNull] Behavior[] children)
		{
			var answer = (Composite)Activator.CreateInstance(compositeType);
			answer.children = children;

			return answer;
		}

		public static Composite Create([NotNull] Type compositeType, [NotNull] Behavior[] children,
			params object[] parameters)
		{
			var answer = (Composite)Activator.CreateInstance(compositeType, parameters);
			answer.children = children;

			return answer;
		}

		public override void Initialize()
		{
			base.Initialize();

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Initialize();
			}
		}

		public override void Dispose()
		{
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Dispose();
			}

			base.Dispose();
		}

		protected override void OnAbort()
		{
			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Abort();
			}

			base.OnAbort();
		}

		internal override void ApplyBlackboard(Blackboard blackboardToApply)
		{
			base.ApplyBlackboard(blackboardToApply);

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].ApplyBlackboard(blackboardToApply);
			}
		}
	}
}
