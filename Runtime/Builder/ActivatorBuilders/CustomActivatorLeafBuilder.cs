// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	/// <summary>
	/// <see cref="Leaf"/> with a setup builder using <see cref="Type"/>.
	/// </summary>
	/// <seealso cref="ActivatorLeafBuilder"/>
	internal sealed class CustomActivatorLeafBuilder : LeafBuilder
	{
		/// <summary>
		/// <see cref="Leaf"/> built type.
		/// </summary>
		[NotNull] private readonly Type m_nodeType;
		/// <summary>
		/// Custom data used in a setup method.
		/// </summary>
		[NotNull] private readonly object[] m_customData;

		/// <param name="nodeType"><see cref="Leaf"/> built type.</param>
		/// <param name="customData">Custom data used in a setup method.</param>
		public CustomActivatorLeafBuilder([NotNull] Type nodeType, [NotNull] object[] customData)
		{
			m_nodeType = nodeType;
			m_customData = customData;
		}

		/// <summary>
		/// <see cref="Leaf"/> built type.
		/// </summary>
		public override Type behaviorType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create(m_nodeType, m_customData);
		}
	}
}
