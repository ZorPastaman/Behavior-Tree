// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
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
