// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Profiling;
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

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf>() where TLeaf : Leaf, INotSetupable, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf>());

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg>(TArg arg) where TLeaf : Leaf, ISetupable<TArg>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg>(arg));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1>(arg0, arg1));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2>(arg0, arg1, arg2));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3>(arg0, arg1, arg2, arg3));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4>(
				arg0, arg1, arg2, arg3, arg4));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				arg0, arg1, arg2, arg3, arg4, arg5));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddLeaf<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TLeaf : Leaf, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddLeaf");
			Profiler.BeginSample(typeof(TLeaf).FullName);

			AddBehaviorBuilder(new LeafBuilder<TLeaf, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator>() where TDecorator : Decorator, INotSetupable, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator>());

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg>(TArg arg)
			where TDecorator : Decorator, ISetupable<TArg>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg>(arg));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1>(arg0, arg1));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2>(arg0, arg1, arg2));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3>(arg0, arg1, arg2, arg3));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4>(
				arg0, arg1, arg2, arg3, arg4));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				arg0, arg1, arg2, arg3, arg4, arg5));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddDecorator<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TDecorator : Decorator, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddDecorator");
			Profiler.BeginSample(typeof(TDecorator).FullName);

			AddBehaviorBuilder(new DecoratorBuilder<TDecorator, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite>() where TComposite : Composite, INotSetupable, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite>());

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg>(TArg arg)
			where TComposite : Composite, ISetupable<TArg>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg>(arg));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1>(TArg0 arg0, TArg1 arg1)
			where TComposite : Composite, ISetupable<TArg0, TArg1>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1>(arg0, arg1));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2>(arg0, arg1, arg2));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3>(arg0, arg1, arg2, arg3));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4>(
				arg0, arg1, arg2, arg3, arg4));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(
				arg0, arg1, arg2, arg3, arg4, arg5));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddComposite<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
			TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
			where TComposite : Composite, ISetupable<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>, new()
		{
			Profiler.BeginSample("TreeBuilder.AddComposite");
			Profiler.BeginSample(typeof(TComposite).FullName);

			AddBehaviorBuilder(new CompositeBuilder<TComposite, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(
				arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddBehavior([NotNull] Type nodeType)
		{
			Profiler.BeginSample("TreeBuilder.AddBehavior");
			Profiler.BeginSample(nodeType.FullName);

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
				Profiler.EndSample();
				Profiler.EndSample();

				throw new Exception();
			}

			AddBehaviorBuilder(behaviorBuilder);

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[NotNull]
		public TreeBuilder AddBehavior([NotNull] Type nodeType, [NotNull, ItemCanBeNull] params object[] customData)
		{
			Profiler.BeginSample("TreeBuilder.AddBehavior");
			Profiler.BeginSample(nodeType.FullName);

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
				Profiler.EndSample();
				Profiler.EndSample();

				throw new Exception();
			}

			AddBehaviorBuilder(behaviorBuilder);

			Profiler.EndSample();
			Profiler.EndSample();

			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull]
		public TreeBuilder Complete()
		{
			Profiler.BeginSample("TreeBuilder.Complete");

			m_currentBuilderIndices.Pop();

			Profiler.EndSample();

			return this;
		}

		[NotNull, Pure]
		public TreeRoot Build()
		{
			Profiler.BeginSample("TreeBuilder.Build()");

			TreeRoot treeRoot = Build(new Blackboard());

			Profiler.EndSample();

			return treeRoot;
		}

		[NotNull, Pure]
		public TreeRoot Build([NotNull] Blackboard blackboard)
		{
			Profiler.BeginSample("TreeBuilder.Build(Blackboard)");

			Behavior rootBehavior = BuildBehavior(0);
			var treeRoot = new TreeRoot(blackboard, rootBehavior);

			Profiler.EndSample();

			return treeRoot;
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
					default:
						throw new Exception();
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

		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		private static Leaf BuildLeaf([NotNull] LeafBuilder leafBuilder)
		{
			return leafBuilder.Build();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
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
