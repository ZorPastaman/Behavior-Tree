// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class BehaviorBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public BehaviorBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Behavior.Create(m_nodeType);
		}
	}
}
