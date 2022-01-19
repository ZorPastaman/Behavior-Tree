// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	/// <summary>
	/// <see cref="Decorator"/> builder using <see cref="Type"/>.
	/// </summary>
	/// <seealso cref="CustomActivatorDecoratorBuilder"/>
	internal sealed class ActivatorDecoratorBuilder : DecoratorBuilder
	{
		/// <summary>
		/// <see cref="Decorator"/> built type.
		/// </summary>
		[NotNull] private readonly Type m_nodeType;

		/// <param name="nodeType"><see cref="Decorator"/> built type.</param>
		public ActivatorDecoratorBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
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
			return Decorator.Create(m_nodeType, child);
		}
	}
}
