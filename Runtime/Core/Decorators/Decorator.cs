// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Initialize()
		{
			base.Initialize();
			child.Initialize();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Dispose()
		{
			child.Dispose();
			base.Dispose();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void OnAbort()
		{
			child.Abort();
			base.OnAbort();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override void ApplyBlackboard(Blackboard blackboardToApply)
		{
			base.ApplyBlackboard(blackboardToApply);
			child.ApplyBlackboard(blackboardToApply);
		}
	}
}
