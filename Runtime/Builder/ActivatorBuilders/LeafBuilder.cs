// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Leaves;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	internal sealed class LeafBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public LeafBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Leaf.Create(m_nodeType);
		}
	}
}
