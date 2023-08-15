// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	/// <summary>
	/// <see cref="Decorator"/> with a setup builder using <see cref="Type"/>.
	/// </summary>
	/// <seealso cref="ActivatorDecoratorBuilder"/>
	internal sealed class CustomActivatorDecoratorBuilder : DecoratorBuilder
	{
		/// <summary>
		/// <see cref="Decorator"/> built type.
		/// </summary>
		[NotNull] private readonly Type m_nodeType;
		/// <summary>
		/// Custom data used in a setup method.
		/// </summary>
		[NotNull] private readonly object[] m_customData;

		/// <param name="nodeType"><see cref="Decorator"/> built type.</param>
		/// <param name="customData">Custom data used in a setup method.</param>
		public CustomActivatorDecoratorBuilder([NotNull] Type nodeType, [NotNull] object[] customData)
		{
			m_nodeType = nodeType;
			m_customData = customData;
		}

		/// <summary>
		/// <see cref="Decorator"/> built type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create(m_nodeType, child, m_customData);
		}
	}
}
