// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	/// <summary>
	/// <see cref="Leaf"/> builder using <see cref="Type"/>.
	/// </summary>
	/// <seealso cref="CustomActivatorLeafBuilder"/>
	internal sealed class ActivatorLeafBuilder : LeafBuilder
	{
		/// <summary>
		/// <see cref="Leaf"/> built type.
		/// </summary>
		[NotNull] private readonly Type m_nodeType;

		/// <param name="nodeType"><see cref="Leaf"/> built type.</param>
		public ActivatorLeafBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
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
			return Leaf.Create(m_nodeType);
		}
	}
}
