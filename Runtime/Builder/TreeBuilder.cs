// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Builder
{
	public sealed class TreeBuilder
	{
		private readonly Stack<BehaviorBuilderWrapper> m_behaviorBuilders = new Stack<BehaviorBuilderWrapper>();
		private BehaviorBuilderWrapper m_root;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeBuilder AddBehavior<T>() where T : Behavior
		{
			return AddBehavior(typeof(T));
		}

		public TreeBuilder AddBehavior([NotNull] Type nodeType)
		{
			IBehaviorBuilder behaviorBuilder;

			if (typeof(Composite).IsAssignableFrom(nodeType))
			{
				behaviorBuilder = new CompositeBuilder(nodeType);
			}
			else if (typeof(Decorator).IsAssignableFrom(nodeType))
			{
				behaviorBuilder = new DecoratorBuilder(nodeType);
			}
			else
			{
				behaviorBuilder = new BehaviorBuilder(nodeType);
			}

			AddBehavior(behaviorBuilder);

			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeBuilder AddBehavior<T>(params object[] customData) where T : Behavior
		{
			return AddBehavior(typeof(T), customData);
		}

		public TreeBuilder AddBehavior([NotNull] Type nodeType, params object[] customData)
		{
			IBehaviorBuilder behaviorBuilder;

			if (typeof(Composite).IsAssignableFrom(nodeType))
			{
				behaviorBuilder = new CustomCompositeBuilder(nodeType, customData);
			}
			else if (typeof(Decorator).IsAssignableFrom(nodeType))
			{
				behaviorBuilder = new CustomDecoratorBuilder(nodeType, customData);
			}
			else
			{
				behaviorBuilder = new CustomBehaviorBuilder(nodeType, customData);
			}

			AddBehavior(behaviorBuilder);

			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeBuilder Finish()
		{
			m_root = m_behaviorBuilders.Pop();
			return this;
		}

		public Tree Build()
		{
			return Build(new Blackboard());
		}

		public Tree Build([NotNull] Blackboard blackboard)
		{
			Behavior root = m_root.Build();
			return new Tree(blackboard, root);
		}

		private void AddBehavior([NotNull] IBehaviorBuilder behaviorBuilder)
		{
			var behaviorBuilderWrapper = new BehaviorBuilderWrapper(behaviorBuilder);

			if (m_behaviorBuilders.Count > 0)
			{
				m_behaviorBuilders.Peek().AddChild(behaviorBuilderWrapper);
			}

			m_behaviorBuilders.Push(behaviorBuilderWrapper);
		}
	}
}
