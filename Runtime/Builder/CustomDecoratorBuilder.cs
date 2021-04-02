// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder
{
	internal sealed class CustomDecoratorBuilder : IBehaviorBuilder
	{
		[NotNull] private readonly Type m_nodeType;
		[NotNull] private readonly object[] m_customData;

		public CustomDecoratorBuilder([NotNull] Type nodeType, [NotNull] object[] customData)
		{
			m_nodeType = nodeType;
			m_customData = customData;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Behavior Build(Behavior[] children)
		{
			return Decorator.Create(m_nodeType, children[0], m_customData);
		}
	}
}
