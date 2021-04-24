// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.DrawingAttributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class NameOverrideAttribute : Attribute
	{
		[NotNull] public readonly string name;
		public readonly int index;

		public NameOverrideAttribute([NotNull] string name, int index)
		{
			this.name = name;
			this.index = index;
		}
	}
}
