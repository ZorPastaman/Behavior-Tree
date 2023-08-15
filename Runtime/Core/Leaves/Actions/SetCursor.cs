// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a cursor using <see cref="Cursor.SetCursor"/>.
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
	/// 		<description>Texture of type <see cref="Texture2D"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Hotspot of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Cursor mode of type <see cref="CursorMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
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
