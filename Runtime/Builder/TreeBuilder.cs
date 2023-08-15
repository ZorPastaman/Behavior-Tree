// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

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
	/// <summary>
	/// Behavior tree builder. The builder helps to create behavior trees in the easy way.
	/// </summary>
	/// <example>
	/// <code>
	/// var treeBuilder = new TreeBuilder();
	/// treeBuilder.AddComposite&lt;Sequence&gt;()
	///		.AddDecorator&lt;Inverter&gt;()
	///			.AddLeaf&lt;CheckBox, string, string, string, LayerMask&gt;(centerPropertyName, halfExtentsPropertyName, orientationPropertyName, layerMask).Complete()
	///		.Complete()
	///		.AddLeaf&lt;DestroyObject, string&gt;(objectPropertyName).Complete()
	/// .Complete();
	/// TreeRoot treeRoot = treeBuilder.Build();
	/// </code>
	/// </example>
	public sealed class TreeBuilder
	{
		/// <summary>
		/// List of all behaviors added to the builder.
		/// </summary>
		private readonly List<BehaviorBuilder> m_behaviorBuilders = new List<BehaviorBuilder>();
		/// <summary>
		/// Path to the last added behavior consisting of indices.
		/// </summary>
		private readonly Stack<int> m_currentBuilderIndices = new Stack<int>();

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that doesn't have a setup method.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the argument of type <typeparamref name="TArg"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg">Argument in a setup method type.</typeparam>
		/// <param name="arg">Argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/> and
		/// <typeparamref name="TArg1"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/> and <typeparamref name="TArg2"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/> and <typeparamref name="TArg3"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/> and
		/// <typeparamref name="TArg4"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/> and <typeparamref name="TArg5"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/>, <typeparamref name="TArg5"/> and <typeparamref name="TArg6"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Leaf"/> behavior of type <typeparamref name="TLeaf"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/>, <typeparamref name="TArg5"/>, <typeparamref name="TArg6"/> and
		/// <typeparamref name="TArg7"/>.
		/// </summary>
		/// <typeparam name="TLeaf"><see cref="Leaf"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after this method.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that doesn't have a setup method.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the argument of type <typeparamref name="TArg"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg">Argument in a setup method type.</typeparam>
		/// <param name="arg">Argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/> and
		/// <typeparamref name="TArg1"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/> and <typeparamref name="TArg2"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/> and <typeparamref name="TArg3"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/> and
		/// <typeparamref name="TArg4"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/> and <typeparamref name="TArg5"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/>, <typeparamref name="TArg5"/> and <typeparamref name="TArg6"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Decorator"/> behavior of type <typeparamref name="TDecorator"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/>, <typeparamref name="TArg5"/>, <typeparamref name="TArg6"/> and
		/// <typeparamref name="TArg7"/>.
		/// </summary>
		/// <typeparam name="TDecorator"><see cref="Decorator"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added a child to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that doesn't have a setup method.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the argument of type <typeparamref name="TArg"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg">Argument in a setup method type.</typeparam>
		/// <param name="arg">Argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/> and
		/// <typeparamref name="TArg1"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/> and <typeparamref name="TArg2"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/> and <typeparamref name="TArg3"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/> and
		/// <typeparamref name="TArg4"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/> and <typeparamref name="TArg5"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/>, <typeparamref name="TArg5"/> and <typeparamref name="TArg6"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Composite"/> behavior of type <typeparamref name="TComposite"/>
		/// that has a setup method with the arguments of types <typeparamref name="TArg0"/>,
		/// <typeparamref name="TArg1"/>, <typeparamref name="TArg2"/>, <typeparamref name="TArg3"/>,
		/// <typeparamref name="TArg4"/>, <typeparamref name="TArg5"/>, <typeparamref name="TArg6"/> and
		/// <typeparamref name="TArg7"/>.
		/// </summary>
		/// <typeparam name="TComposite"><see cref="Composite"/> type.</typeparam>
		/// <typeparam name="TArg0">First argument in a setup method type.</typeparam>
		/// <typeparam name="TArg1">Second argument in a setup method type.</typeparam>
		/// <typeparam name="TArg2">Third argument in a setup method type.</typeparam>
		/// <typeparam name="TArg3">Fourth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg4">Fifth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg5">Sixth argument in a setup method type.</typeparam>
		/// <typeparam name="TArg6">Seventh argument in a setup method type.</typeparam>
		/// <typeparam name="TArg7">Eighth argument in a setup method type.</typeparam>
		/// <param name="arg0">First argument in a setup method.</param>
		/// <param name="arg1">Second argument in a setup method.</param>
		/// <param name="arg2">Third argument in a setup method.</param>
		/// <param name="arg3">Fourth argument in a setup method.</param>
		/// <param name="arg4">Fifth argument in a setup method.</param>
		/// <param name="arg5">Sixth argument in a setup method.</param>
		/// <param name="arg6">Seventh argument in a setup method.</param>
		/// <param name="arg7">Eighth argument in a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Behavior"/> of type <paramref name="nodeType"/> that doesn't have a setup method.
		/// </summary>
		/// <param name="nodeType"><see cref="Behavior"/> type.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="nodeType"/> isn't a subclass of <see cref="Leaf"/>, <see cref="Decorator"/>
		/// or <see cref="Composite"/>.
		/// </exception>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Adds a new <see cref="Behavior"/> of type <paramref name="nodeType"/> that has a setup method with
		/// arguments of types of <paramref name="customData"/> elements.
		/// </summary>
		/// <param name="nodeType"><see cref="Behavior"/> type.</param>
		/// <param name="customData">Arguments for a setup method.</param>
		/// <returns>Itself.</returns>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="nodeType"/> isn't a subclass of <see cref="Leaf"/>, <see cref="Decorator"/>
		/// or <see cref="Composite"/>.
		/// </exception>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
		/// <remarks>
		/// Call <see cref="Complete"/> after you have added all children to this behavior.
		/// </remarks>
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

		/// <summary>
		/// Marks a current behavior complete and sets a pointer to its parent if it's not a root.
		/// </summary>
		/// <returns>Itself.</returns>
		/// <exception cref="InvalidOperationException">Thrown when called more than
		/// <see cref="AddBehavior(System.Type)"/> or its variations.</exception>
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

		/// <summary>
		/// Clears all the tree.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			m_behaviorBuilders.Clear();
			m_currentBuilderIndices.Clear();
		}

		/// <summary>
		/// Builds a new behavior tree based on the added behaviors and its connections.
		/// </summary>
		/// <returns><see cref="TreeRoot"/> of the built behavior tree.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when you try to build an incomplete behavior tree.
		/// </exception>
		/// <remarks>
		/// This method creates a new <see cref="Blackboard"/> and sets it to the built behavior tree.
		/// </remarks>
		[NotNull, Pure]
		public TreeRoot Build()
		{
			Profiler.BeginSample("TreeBuilder.Build()");

			TreeRoot treeRoot = Build(new Blackboard());

			Profiler.EndSample();

			return treeRoot;
		}

		/// <summary>
		/// Builds a new behavior tree based on the added behaviors and its connections.
		/// </summary>
		/// <param name="blackboard"><see cref="Blackboard"/> that is set to the built behavior tree.</param>
		/// <returns><see cref="TreeRoot"/> of the built behavior tree.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when you try to build an incomplete behavior tree.
		/// </exception>
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

		/// <summary>
		/// Adds <paramref name="behaviorBuilder"/> as a child to the current behavior
		/// or sets it as a root if it's first.
		/// </summary>
		/// <param name="behaviorBuilder">Added <see cref="BehaviorBuilder"/>.</param>
		/// <exception cref="InvalidOperationException">Thrown when you try to add a second root behavior.</exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when tou try to add a child to a <see cref="Leaf"/>.
		/// </exception>
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
						AddCompositeChild(compositeBuilder, index);
						break;
					case DecoratorBuilder decoratorBuilder:
						AddDecoratorChild(decoratorBuilder, index);
						break;
					default:
						throw new InvalidOperationException($"Failed to add a child to a {nameof(Leaf)}. Only {nameof(Composite)} and {nameof(Decorator)} support children.");
				}
			}

			m_behaviorBuilders.Add(behaviorBuilder);
			m_currentBuilderIndices.Push(index);
		}

		/// <summary>
		/// Adds a child by its <paramref name="childIndex"/>
		/// to a <see cref="Composite"/> of <paramref name="compositeBuilder"/>.
		/// </summary>
		/// <param name="compositeBuilder"><see cref="CompositeBuilder"/>.</param>
		/// <param name="childIndex">Child index.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AddCompositeChild([NotNull] CompositeBuilder compositeBuilder, int childIndex)
		{
			compositeBuilder.AddChildIndex(childIndex);
		}

		/// <summary>
		/// Adds a child by its <paramref name="childIndex"/>
		/// to a <see cref="Decorator"/> of <paramref name="decoratorBuilder"/>.
		/// </summary>
		/// <param name="decoratorBuilder"><see cref="DecoratorBuilder"/>.</param>
		/// <param name="childIndex">Child index.</param>
		/// <exception cref="InvalidOperationException">
		/// Thrown when <paramref name="decoratorBuilder"/> already has a child.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AddDecoratorChild([NotNull] DecoratorBuilder decoratorBuilder, int childIndex)
		{
#if DEBUG
			if (decoratorBuilder.childIndex >= 0)
			{
				throw new InvalidOperationException($"Failed to set a child to a {nameof(Decorator)}. It already has a child at index {decoratorBuilder.childIndex}.");
			}
#endif

			decoratorBuilder.childIndex = childIndex;
		}

		/// <summary>
		/// Builds a <see cref="Behavior"/> at index <paramref name="index"/>.
		/// </summary>
		/// <param name="index">Index of the built <see cref="Behavior"/>.</param>
		/// <returns>Built <see cref="Behavior"/>.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when there's no behavior at index <paramref name="index"/>.
		/// </exception>
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

		/// <summary>
		/// Builds a <see cref="Leaf"/> with the <paramref name="leafBuilder"/>.
		/// </summary>
		/// <param name="leafBuilder">Used <see cref="LeafBuilder"/>.</param>
		/// <returns>Built <see cref="Leaf"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		private static Leaf BuildLeaf([NotNull] LeafBuilder leafBuilder)
		{
			return leafBuilder.Build();
		}

		/// <summary>
		/// Builds a <see cref="Decorator"/> with the <paramref name="decoratorBuilder"/>.
		/// </summary>
		/// <param name="decoratorBuilder">Used <see cref="DecoratorBuilder"/>.</param>
		/// <returns>Built <see cref="Decorator"/>.</returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when you try to build a <see cref="Decorator"/> without a child.
		/// </exception>
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

		/// <summary>
		/// Builds a <see cref="Composite"/> with the <paramref name="compositeBuilder"/>.
		/// </summary>
		/// <param name="compositeBuilder">Used <see cref="CompositeBuilder"/>.</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when you try to build a <see cref="Composite"/> without children.
		/// </exception>
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
			if (index < 0 | index >= m_behaviorBuilders.Count)
			{
				return;
			}

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
