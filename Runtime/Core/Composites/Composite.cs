// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Composites
{
	public abstract class Composite : Behavior
	{
		[NotNull] protected readonly Behavior[] children;

		protected Composite([NotNull] Behavior[] children)
		{
			this.children = children;
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
			base.Dispose();

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Dispose();
			}
		}

		protected override void OnAbort()
		{
			base.OnAbort();

			for (int i = 0, count = children.Length; i < count; ++i)
			{
				children[i].Abort();
			}
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
