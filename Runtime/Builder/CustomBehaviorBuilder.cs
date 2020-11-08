// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class CustomBehaviorBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;
		[NotNull] private readonly object[] m_customData;

		public CustomBehaviorBuilder([NotNull] Type nodeType, [NotNull] object[] customData)
		{
			m_nodeType = nodeType;
			m_customData = customData;
		}

		public Behavior Build(Behavior[] children)
		{
			return (Behavior)Activator.CreateInstance(m_nodeType, m_customData);
		}
	}
}
