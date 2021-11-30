// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.BehaviorTree.Builder.ActivatorBuilders;
using Zor.BehaviorTree.Builder.GenericBuilders;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;
using Zor.BehaviorTree.Core.Decorators;
using Zor.BehaviorTree.Core.Leaves;
using Zor.BehaviorTree.Debugging;
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

				throw new ArgumentException($"{nodeType} isn't a subclass of {nameof(Leaf)}, {nameof(Decorator)} or {nameof(Composite)}.", nameof(nodeType));
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

				throw new ArgumentException($"{nodeType} isn't a subclass of {nameof(Leaf)}, {nameof(Decorator)} or {nameof(Composite)}.", nameof(nodeType));
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

			BehaviorTreeDebug.Log($"[TreeBuilder] Complete a behavior at level {m_currentBuilderIndices.Count - 1}");

#if DEBUG
			if (m_currentBuilderIndices.Count == 0)
			{
				throw new InvalidOperationException(
					$"{nameof(Complete)} is called more than {nameof(AddBehavior)} or its generic variations.");
			}
#endif

			m_currentBuilderIndices.Pop();

			Profiler.EndSample();

			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			m_behaviorBuilders.Clear();
			m_currentBuilderIndices.Clear();
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

			BehaviorTreeDebug.Log("Start building a tree");

			Behavior rootBehavior = BuildBehavior(0);
			var treeRoot = new TreeRoot(blackboard, rootBehavior);

			BehaviorTreeDebug.Log("Finish building a tree");

			Profiler.EndSample();

			return treeRoot;
		}

		private void AddBehaviorBuilder([NotNull] BehaviorBuilder behaviorBuilder)
		{
			int index = m_behaviorBuilders.Count;

			BehaviorTreeDebug.Log($"[TreeBuilder] Add a behavior of type {behaviorBuilder.behaviorType} at index {index} at level {m_currentBuilderIndices.Count}");

			if (index > 0)
			{
#if DEBUG
				if (m_currentBuilderIndices.Count == 0)
				{
					throw new InvalidOperationException($"Failed to add a behavior of type {behaviorBuilder.behaviorType} as a root because the tree builder already has a root.");
				}
#endif

				BehaviorBuilder currentBuilder = m_behaviorBuilders[m_currentBuilderIndices.Peek()];

				switch (currentBuilder)
				{
					case CompositeBuilder compositeBuilder:
						AddCompositeBuilder(compositeBuilder, index);
						break;
					case DecoratorBuilder decoratorBuilder:
						AddDecoratorBuilder(decoratorBuilder, index);
						break;
					default:
						throw new InvalidOperationException($"Failed to add a child to a {nameof(Leaf)}. Only {nameof(Composite)} and {nameof(Decorator)} support children.");
				}
			}

			m_behaviorBuilders.Add(behaviorBuilder);
			m_currentBuilderIndices.Push(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AddCompositeBuilder([NotNull] CompositeBuilder compositeBuilder, int index)
		{
			compositeBuilder.AddChildIndex(index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AddDecoratorBuilder([NotNull] DecoratorBuilder decoratorBuilder, int index)
		{
#if DEBUG
			if (decoratorBuilder.childIndex >= 0)
			{
				throw new InvalidOperationException($"Failed to set a child to a {nameof(Decorator)}. It already has a child at index {decoratorBuilder.childIndex}.");
			}
#endif

			decoratorBuilder.childIndex = index;
		}

		[NotNull, Pure]
		private Behavior BuildBehavior(int index)
		{
#if DEBUG
			if (index < 0 || index >= m_behaviorBuilders.Count)
			{
				throw new InvalidOperationException($"Failed to build a behavior tree. There's no behavior at index {index}.");
			}
#endif

			BehaviorBuilder behaviorBuilder = m_behaviorBuilders[index];
			Behavior result;

			BehaviorTreeDebug.Log($"[TreeBuilder] Start building a behavior of type {behaviorBuilder.behaviorType} at index {index}");

			switch (behaviorBuilder)
			{
				case LeafBuilder leafBuilder:
					result = BuildLeaf(leafBuilder);
					break;
				case DecoratorBuilder decoratorBuilder:
					result = BuildDecorator(decoratorBuilder);
					break;
				case CompositeBuilder compositeBuilder:
					result = BuildComposite(compositeBuilder);
					break;
				default:
					throw new InvalidOperationException($"Failed to build a behavior at index {index} due to an unsupported builder. Is it possible?");
			}

			BehaviorTreeDebug.Log($"[TreeBuilder] Finish building a behavior of type {behaviorBuilder.behaviorType} at index {index}");

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		private static Leaf BuildLeaf([NotNull] LeafBuilder leafBuilder)
		{
			return leafBuilder.Build();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		private Decorator BuildDecorator([NotNull] DecoratorBuilder decoratorBuilder)
		{
#if DEBUG
			if (decoratorBuilder.childIndex < 0)
			{
				throw new InvalidOperationException($"Failed to build a {nameof(Decorator)}. It doesn't have a child.");
			}
#endif

			Behavior child = BuildBehavior(decoratorBuilder.childIndex);
			return decoratorBuilder.Build(child);
		}

		[NotNull, Pure]
		private Behavior BuildComposite([NotNull] CompositeBuilder compositeBuilder)
		{
			int childCount = compositeBuilder.childCount;

#if DEBUG
			if (childCount == 0)
			{
				throw new InvalidOperationException($"Failed to build a {nameof(Composite)}. It doesn't have children.");
			}

			if (childCount < 2)
			{
				BehaviorTreeDebug.LogWarning($"[TreeBuilder] Composite of type {compositeBuilder.behaviorType} has less than 2 children");
			}
#endif

			var behaviors = new Behavior[childCount];

			for (int i = 0; i < childCount; ++i)
			{
				int childIndex = compositeBuilder.GetChildIndex(i);
				behaviors[i] = BuildBehavior(childIndex);
			}

			return compositeBuilder.Build(behaviors);
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder("TreeBuilder:\n");
			BehaviorToString(stringBuilder, 0, 0);

			return stringBuilder.ToString();
		}

		private void BehaviorToString([NotNull] StringBuilder stringBuilder, int index, int level)
		{
			for (int i = 0; i < level; ++i)
			{
				stringBuilder.Append('\t');
			}

			BehaviorBuilder behaviorBuilder = m_behaviorBuilders[index];
			stringBuilder.AppendLine(behaviorBuilder.behaviorType.ToString());

			switch (behaviorBuilder)
			{
				case DecoratorBuilder decoratorBuilder:
					if (decoratorBuilder.childIndex >= 0)
					{
						BehaviorToString(stringBuilder, decoratorBuilder.childIndex, level + 1);
					}
					break;
				case CompositeBuilder compositeBuilder:
					for (int i = 0, count = compositeBuilder.childCount; i < count; ++i)
					{
						int child = compositeBuilder.GetChildIndex(i);
						BehaviorToString(stringBuilder, child, level + 1);
					}
					break;
			}
		}
	}
}
