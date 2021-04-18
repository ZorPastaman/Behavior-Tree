// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;
using Zor.BehaviorTree.Core.Decorators;

namespace Zor.BehaviorTree.Builder.ActivatorBuilders
{
	internal sealed class ActivatorDecoratorBuilder : DecoratorBuilder
	{
		[NotNull] private readonly Type m_nodeType;

		public ActivatorDecoratorBuilder([NotNull] Type nodeType)
		{
			m_nodeType = nodeType;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override Decorator Build(Behavior child)
		{
			return Decorator.Create(m_nodeType, child);
		}
	}
}
