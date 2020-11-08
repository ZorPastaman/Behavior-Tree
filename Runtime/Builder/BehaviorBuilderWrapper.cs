// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Collections.Generic;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class BehaviorBuilderWrapper
	{
		[NotNull] private readonly IBehaviorBuilder m_behaviorBuilder;
		[NotNull] private readonly List<BehaviorBuilderWrapper> m_children = new List<BehaviorBuilderWrapper>();

		public BehaviorBuilderWrapper([NotNull] IBehaviorBuilder behaviorBuilder)
		{
			m_behaviorBuilder = behaviorBuilder;
		}

		public void AddChild([NotNull] BehaviorBuilderWrapper behaviorBuilderWrapper)
		{
			m_children.Add(behaviorBuilderWrapper);
		}

		public Behavior Build()
		{
			int childrenCount = m_children.Count;
			var children = new Behavior[childrenCount];

			for (int i = 0; i < childrenCount; ++i)
			{
				children[i] = m_children[i].Build();
			}

			return m_behaviorBuilder.Build(children);
		}
	}
}
