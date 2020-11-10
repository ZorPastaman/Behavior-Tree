// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Composites;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class CompositeBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public CompositeBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Composite.Create(m_nodeType, children);
		}
	}
}
