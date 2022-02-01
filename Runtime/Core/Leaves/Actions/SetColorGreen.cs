// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="Color.g"/> and sets a new color into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a color of type <see cref="Color"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Green of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Color"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SetColorGreen : Action,
		ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>, ISetupable<string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private float m_green;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colorPropertyName, float green, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(colorPropertyName, green, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float, string>.Setup(string colorPropertyName, float green, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName), green,
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colorPropertyName, float green,
			BlackboardPropertyName resultPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_green = green;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_colorPropertyName, out Color color))
			{
				color.g = m_green;
				blackboard.SetStructValue(m_resultPropertyName, color);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
