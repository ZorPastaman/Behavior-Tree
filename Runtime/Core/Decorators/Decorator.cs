// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public abstract class Decorator : Behavior
	{
		[NotNull] protected readonly Behavior child;

		protected Decorator([NotNull] Behavior child)
		{
			this.child = child;
		}

		public override void Initialize()
		{
			base.Initialize();
			child.Initialize();
		}

		public override void Dispose()
		{
			base.Dispose();
			child.Dispose();
		}

		protected override void OnAbort()
		{
			base.OnAbort();
			child.Abort();
		}

		internal override void ApplyBlackboard(Blackboard blackboardToApply)
		{
			base.ApplyBlackboard(blackboardToApply);
			child.ApplyBlackboard(blackboardToApply);
		}
	}
}
