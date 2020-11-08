// Copyright (c) 2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;
using Zor.BehaviorTree.Core;

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

		public Behavior Build(Behavior[] children)
		{
			int customDataLength = m_customData.Length;
			var args = new object[customDataLength + 1];
			args[0] = children[0];
			Array.Copy(m_customData, 0, args, 1, customDataLength);

			return (Behavior)Activator.CreateInstance(m_nodeType, args);
		}
	}
}
