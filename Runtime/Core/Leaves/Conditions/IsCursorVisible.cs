// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using JetBrains.Annotations;
using UnityEngine;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a cursor is visible.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the cursor is visible.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the cursor isn't visible.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsCursorVisible : Condition, INotSetupable
	{
		[Pure]
		protected override Status Execute()
		{
			return StateToStatusHelper.ConditionToStatus(Cursor.visible);
		}
	}
}
