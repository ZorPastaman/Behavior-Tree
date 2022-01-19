// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;
using JetBrains.Annotations;

namespace Zor.BehaviorTree.DrawingAttributes
{
	/// <summary>
	/// Sets a search group in a behavior tree graph view for
	/// <see cref="Zor.BehaviorTree.Serialization.SerializedBehaviors.SerializedBehavior{T}"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SearchGroupAttribute : Attribute
	{
		/// <summary>
		/// Group path. This may have slashes to create subfolders.
		/// </summary>
		[NotNull] public readonly string groupPath;

		/// <param name="groupPath">Group path. This may have slashes to create subfolders.</param>
		public SearchGroupAttribute([NotNull] string groupPath)
		{
			this.groupPath = groupPath;
		}
	}
}
