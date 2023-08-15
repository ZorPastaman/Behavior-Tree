// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	/// <summary>
	/// <see cref="Composite"/> with a setup builder using <see cref="Type"/>.
	/// </summary>
	/// <seealso cref="ActivatorCompositeBuilder"/>
	internal sealed class CustomActivatorCompositeBuilder : CompositeBuilder
	{
		/// <summary>
		/// <see cref="Composite"/> built type.
		/// </summary>
		[NotNull] private readonly Type m_nodeType;
		/// <summary>
		/// Custom data used in a setup method.
		/// </summary>
		[NotNull] private readonly object[] m_customData;

		/// <param name="nodeType"><see cref="Composite"/> built type.</param>
		/// <param name="customData">Custom data used in a setup method.</param>
		public CustomActivatorCompositeBuilder([NotNull] Type nodeType, [NotNull] object[] customData)
		{
			m_nodeType = nodeType;
			m_customData = customData;
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
			return Composite.Create(m_nodeType, children, m_customData);
		}
	}
}
