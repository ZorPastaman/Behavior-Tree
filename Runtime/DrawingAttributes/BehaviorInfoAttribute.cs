// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System;

namespace Zor.BehaviorTree.DrawingAttributes
{
	/// <summary>
	/// Allows a field in a <see cref="Zor.BehaviorTree.Core.Behavior"/> to be drawn in a behavior tree graph view.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class BehaviorInfoAttribute : Attribute
	{
	}
}
