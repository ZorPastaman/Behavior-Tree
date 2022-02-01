// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a cursor visibility using <see cref="Cursor.visible"/>.
	/// </para>
	/// <para>
	/// Always returns <see cref="Status.Success"/> in its tick.
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Visible of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetCursorVisible : Action, ISetupable<bool>
	{
		[BehaviorInfo] private bool m_visible;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<bool>.Setup(bool visible)
		{
			m_visible = visible;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			Cursor.visible = m_visible;
			return Status.Success;
		}
	}
}
