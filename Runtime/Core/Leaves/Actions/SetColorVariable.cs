// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="Color"/> into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a red of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a green of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a blue of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of an alpha of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a color of type <see cref="Color"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SetColorVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_redPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_greenPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_bluePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_alphaPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName redPropertyName, BlackboardPropertyName greenPropertyName,
			BlackboardPropertyName bluePropertyName, BlackboardPropertyName alphaPropertyName,
			BlackboardPropertyName colorPropertyName)
		{
			SetupInternal(redPropertyName, greenPropertyName, bluePropertyName, alphaPropertyName, colorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string redPropertyName, string greenPropertyName,
			string bluePropertyName, string alphaPropertyName, string colorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(redPropertyName), new BlackboardPropertyName(greenPropertyName),
				new BlackboardPropertyName(bluePropertyName), new BlackboardPropertyName(alphaPropertyName),
				new BlackboardPropertyName(colorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName redPropertyName, BlackboardPropertyName greenPropertyName,
			BlackboardPropertyName bluePropertyName, BlackboardPropertyName alphaPropertyName,
			BlackboardPropertyName colorPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_redPropertyName = redPropertyName;
			m_greenPropertyName = greenPropertyName;
			m_bluePropertyName = bluePropertyName;
			m_alphaPropertyName = alphaPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_redPropertyName, out float red) &
				blackboard.TryGetStructValue(m_greenPropertyName, out float green) &
				blackboard.TryGetStructValue(m_bluePropertyName, out float blue) &
				blackboard.TryGetStructValue(m_alphaPropertyName, out float alpha))
			{
				blackboard.SetStructValue(m_colorPropertyName, new Color(red, green, blue, alpha));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
