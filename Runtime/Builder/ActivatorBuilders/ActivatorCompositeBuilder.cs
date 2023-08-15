// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	/// <summary>
	/// <see cref="Composite"/> builder using <see cref="Type"/>.
	/// </summary>
	/// <seealso cref="CustomActivatorCompositeBuilder"/>
	internal sealed class ActivatorCompositeBuilder : CompositeBuilder
	{
		/// <summary>
		/// <see cref="Composite"/> built type.
		/// </summary>
		[NotNull] private readonly Type m_nodeType;

		/// <param name="nodeType"><see cref="Composite"/> built type.</param>
		public ActivatorCompositeBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		/// <summary>
		/// <see cref="Composite"/> built type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Composite Build(Behavior[] children)
		{
			return Composite.Create(m_nodeType, children);
		}
	}
}
