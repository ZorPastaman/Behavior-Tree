// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	internal sealed class CustomActivatorLeafBuilder : LeafBuilder
	{
		[NotNull] private readonly Type m_nodeType;
		[NotNull] private readonly object[] m_customData;

		public CustomActivatorLeafBuilder([NotNull] Type nodeType, [NotNull] object[] customData)
		{
			m_nodeType = nodeType;
			m_customData = customData;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Leaf Build()
		{
			return Leaf.Create(m_nodeType, m_customData);
		}
	}
}
