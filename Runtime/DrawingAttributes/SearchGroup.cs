// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.DrawingAttributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SearchGroup : Attribute
	{
		[NotNull] public readonly string groupPath;

		public SearchGroup([NotNull] string groupPath)
		{
			this.groupPath = groupPath;
		}
	}
}
