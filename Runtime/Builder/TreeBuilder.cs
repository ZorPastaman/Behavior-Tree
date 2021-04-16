// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Builder.ActivatorBuilders;
using Zor.BehaviorTree.Builder.GenericBuilders;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Builder
{
	public sealed class TreeBuilder
	{
		private readonly Stack<BehaviorBuilderWrapper> m_behaviorBuilders = new Stack<BehaviorBuilderWrapper>();
		private BehaviorBuilderWrapper m_rootLeaf;

		public TreeBuilder AddLeaf<TLeaf>() where TLeaf : Leaf, INotSetupable, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf>());
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg>(TArg arg) where TLeaf : Leaf, ISetupable<TArg>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg>(arg));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1>(arg0, arg1));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2>(arg0, arg1, arg2));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3>(arg0, arg1, arg2, arg3));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
				arg0, arg1, arg2, arg3, arg4));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				arg0, arg1, arg2, arg3, arg4, arg5));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6));
			return this;
		}

		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
			return this;
		}
		
		public TreeBuilder AddDecorator<TDecorator>() where TDecorator : Decorator, INotSetupable, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator>());
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg>(TArg arg) where TDecorator : Decorator, ISetupable<TArg>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg>(arg));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1>(arg0, arg1));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2>(arg0, arg1, arg2));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3>(arg0, arg1, arg2, arg3));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(
				arg0, arg1, arg2, arg3, arg4));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				arg0, arg1, arg2, arg3, arg4, arg5));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6));
			return this;
		}

		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
			return this;
		}
		
		public TreeBuilder AddComposite<TComposite>() where TComposite : Composite, INotSetupable, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite>());
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg>(TArg arg) where TComposite : Composite, ISetupable<TArg>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg>(arg));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TComposite : Composite, ISetupable<TArg0, TArg1>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1>(arg0, arg1));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2>(arg0, arg1, arg2));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3>(arg0, arg1, arg2, arg3));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
				arg0, arg1, arg2, arg3, arg4));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				arg0, arg1, arg2, arg3, arg4, arg5));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6));
			return this;
		}

		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
			return this;
		}

		public TreeBuilder AddBehavior([NotNull] Type nodeType)
		{
			IBehaviorBuilder behaviorBuilder;

			if (nodeType.IsSubclassOf(typeof(Composite)))
			{
				behaviorBuilder = new CompositeBuilder(nodeType);
			}
			else if (nodeType.IsSubclassOf(typeof(Decorator)))
			{
				behaviorBuilder = new DecoratorBuilder(nodeType);
			}
			else if (nodeType.IsSubclassOf(typeof(Leaf)))
			{
				behaviorBuilder = new LeafBuilder(nodeType);
			}
			else
			{
				return this;
			}

			AddBehaviorBuilder(behaviorBuilder);

			return this;
		}

		public TreeBuilder AddBehavior([NotNull] Type nodeType, params object[] customData)
		{
			IBehaviorBuilder behaviorBuilder;

			if (nodeType.IsSubclassOf(typeof(Composite)))
			{
				behaviorBuilder = new CustomCompositeBuilder(nodeType, customData);
			}
			else if (nodeType.IsSubclassOf(typeof(Decorator)))
			{
				behaviorBuilder = new CustomDecoratorBuilder(nodeType, customData);
			}
			else if (nodeType.IsSubclassOf(typeof(Leaf)))
			{
				behaviorBuilder = new CustomLeafBuilder(nodeType, customData);
			}
			else
			{
				return this;
			}

			AddBehaviorBuilder(behaviorBuilder);

			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeBuilder Finish()
		{
			m_rootLeaf = m_behaviorBuilders.Pop();
			return this;
		}

		public TreeRoot Build()
		{
			return Build(new Blackboard());
		}

		public TreeRoot Build([NotNull] Blackboard blackboard)
		{
			Behavior rootLeaf = m_rootLeaf.Build();
			return new TreeRoot(blackboard, rootLeaf);
		}

		private void AddBehaviorBuilder([NotNull] IBehaviorBuilder behaviorBuilder)
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
