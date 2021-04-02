// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Decorators
{
	public abstract class Decorator : Behavior
	{
		[NotNull] protected Behavior child;

		public static T Create<T>([NotNull] Behavior child) where T : Decorator, new()
		{
			return new T {child = child};
		}

		public static Decorator Create([NotNull] Type decoratorType, [NotNull] Behavior child)
		{
			var answer = (Decorator)Activator.CreateInstance(decoratorType);
			answer.child = child;

			return answer;
		}

		public static Decorator Create([NotNull] Type decoratorType, [NotNull] Behavior child,
			params object[] parameters)
		{
			var answer = (Decorator)Activator.CreateInstance(decoratorType, parameters);
			answer.child = child;

			return answer;
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
