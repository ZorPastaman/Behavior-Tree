// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a cursor using <see cref="Cursor.SetCursor"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if there's all the data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Error"/> </term>
	/// 		<description>if there's no data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of a texture of type <see cref="Texture2D"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a hotspot of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a cursor mode of type <see cref="CursorMode"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetCursorVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_texturePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hotspotPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_cursorModePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName texturePropertyName, BlackboardPropertyName hotspotPropertyName,
			BlackboardPropertyName cursorModePropertyName)
		{
			SetupInternal(texturePropertyName, hotspotPropertyName, cursorModePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string texturePropertyName, string hotspotPropertyName,
			string cursorModePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(texturePropertyName),
				new BlackboardPropertyName(hotspotPropertyName), new BlackboardPropertyName(cursorModePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName texturePropertyName,
			BlackboardPropertyName hotspotPropertyName, BlackboardPropertyName cursorModePropertyName)
		{
			m_texturePropertyName = texturePropertyName;
			m_hotspotPropertyName = hotspotPropertyName;
			m_cursorModePropertyName = cursorModePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_texturePropertyName, out Texture2D texture) & texture != null &
				blackboard.TryGetStructValue(m_hotspotPropertyName, out Vector2 hotspot) &
				blackboard.TryGetStructValue(m_cursorModePropertyName, out CursorMode cursorMode))
			{
				Cursor.SetCursor(texture, hotspot, cursorMode);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
