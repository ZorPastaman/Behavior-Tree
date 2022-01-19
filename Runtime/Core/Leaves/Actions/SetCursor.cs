// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetCursor : Action, ISetupable<Texture2D, Vector2, CursorMode>
	{
		[BehaviorInfo] private Texture2D m_texture;
		[BehaviorInfo] private Vector2 m_hotspot;
		[BehaviorInfo] private CursorMode m_cursorMode;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<Texture2D, Vector2, CursorMode>.Setup(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			m_texture = texture;
			m_hotspot = hotspot;
			m_cursorMode = cursorMode;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			Cursor.SetCursor(m_texture, m_hotspot, m_cursorMode);
			return Status.Success;
		}
	}
}
