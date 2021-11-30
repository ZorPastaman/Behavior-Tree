// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	internal sealed class ActivatorLeafBuilder : LeafBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public ActivatorLeafBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

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
