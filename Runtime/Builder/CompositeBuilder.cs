// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class CompositeBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public CompositeBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		public Behavior Build(Blackboard blackboard, Behavior[] children)
		{
			return (Behavior)Activator.CreateInstance(m_nodeType, blackboard, children);
		}
	}
}
