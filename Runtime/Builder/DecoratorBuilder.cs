// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class DecoratorBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public DecoratorBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		public Behavior Build(Behavior[] children)
		{
			return (Behavior)Activator.CreateInstance(m_nodeType, children[0]);
		}
	}
}
