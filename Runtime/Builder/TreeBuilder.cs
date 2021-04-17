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
		private readonly List<BehaviorBuilder> m_behaviorBuilders = new List<BehaviorBuilder>();
		private readonly Stack<int> m_currentBuilderIndices = new Stack<int>();

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

		public TreeBuilder AddDecorator<TDecorator, TArg>(TArg arg)
			where TDecorator : Decorator, ISetupable<TArg>, new()
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

		public TreeBuilder AddComposite<TComposite, TArg>(TArg arg)
			where TComposite : Composite, ISetupable<TArg>, new()
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
			BehaviorBuilder behaviorBuilder;

			if (nodeType.IsSubclassOf(typeof(Leaf)))
			{
				behaviorBuilder = new ActivatorLeafBuilder(nodeType);
			}
			else if (nodeType.IsSubclassOf(typeof(Decorator)))
			{
				behaviorBuilder = new ActivatorDecoratorBuilder(nodeType);
			}
			else if (nodeType.IsSubclassOf(typeof(Composite)))
			{
				behaviorBuilder = new ActivatorCompositeBuilder(nodeType);
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
			BehaviorBuilder behaviorBuilder;

			if (nodeType.IsSubclassOf(typeof(Leaf)))
			{
				behaviorBuilder = new CustomActivatorLeafBuilder(nodeType, customData);
			}
			else if (nodeType.IsSubclassOf(typeof(Decorator)))
			{
				behaviorBuilder = new CustomActivatorDecoratorBuilder(nodeType, customData);
			}
			else if (nodeType.IsSubclassOf(typeof(Composite)))
			{
				behaviorBuilder = new CustomActivatorCompositeBuilder(nodeType, customData);
			}
			else
			{
				return this;
			}

			AddBehaviorBuilder(behaviorBuilder);

			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TreeBuilder Complete()
		{
			m_currentBuilderIndices.Pop();
			return this;
		}

		public TreeRoot Build()
		{
			return Build(new Blackboard());
		}

		public TreeRoot Build([NotNull] Blackboard blackboard)
		{
			Behavior rootBehavior = BuildBehavior(0);
			return new TreeRoot(blackboard, rootBehavior);
		}

		private void AddBehaviorBuilder([NotNull] BehaviorBuilder behaviorBuilder)
		{
			int index = m_behaviorBuilders.Count;

			if (index > 0)
			{
				BehaviorBuilder currentBuilder = m_behaviorBuilders[m_currentBuilderIndices.Peek()];

				switch (currentBuilder)
				{
					case CompositeBuilder compositeBuilder:
						compositeBuilder.AddChildIndex(index);
						break;
					case DecoratorBuilder decoratorBuilder:
						decoratorBuilder.childIndex = index;
						break;
				}
			}

			m_behaviorBuilders.Add(behaviorBuilder);
			m_currentBuilderIndices.Push(index);
		}

		[NotNull, Pure]
		private Behavior BuildBehavior(int index)
		{
			BehaviorBuilder behaviorBuilder = m_behaviorBuilders[index];

			switch (behaviorBuilder)
			{
				case LeafBuilder leafBuilder:
					return BuildLeaf(leafBuilder);
				case DecoratorBuilder decoratorBuilder:
					return BuildDecorator(decoratorBuilder);
				case CompositeBuilder compositeBuilder:
					return BuildComposite(compositeBuilder);
				default:
					throw new Exception();
			}
		}

		[NotNull, MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private static Leaf BuildLeaf([NotNull] LeafBuilder leafBuilder)
		{
			return leafBuilder.Build();
		}

		[NotNull, MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		private Decorator BuildDecorator([NotNull] DecoratorBuilder decoratorBuilder)
		{
			Behavior child = BuildBehavior(decoratorBuilder.childIndex);
			return decoratorBuilder.Build(child);
		}

		[NotNull, Pure]
		private Behavior BuildComposite([NotNull] CompositeBuilder compositeBuilder)
		{
			int childCount = compositeBuilder.childCount;
			var behaviors = new Behavior[childCount];

			for (int i = 0; i < childCount; ++i)
			{
				int childIndex = compositeBuilder.GetChildIndex(i);
				behaviors[i] = BuildBehavior(childIndex);
			}

			return compositeBuilder.Build(behaviors);
		}
	}
}
