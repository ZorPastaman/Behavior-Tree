// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a <see cref="Color.b"/> is less than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the color blue is less than the specified blue.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the color blue isn't less than the specified blue.</description>
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
	/// 		<description>Property name of blue of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsColorBlueLessVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_bluePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colorPropertyName, BlackboardPropertyName bluePropertyName)
		{
			SetupInternal(colorPropertyName, bluePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string colorPropertyName, string bluePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName), new BlackboardPropertyName(bluePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colorPropertyName, BlackboardPropertyName bluePropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_bluePropertyName = bluePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_colorPropertyName, out Color color) &
				blackboard.TryGetStructValue(m_bluePropertyName, out float blue);
			bool isLess = color.b < blue;

			return StateToStatusHelper.ConditionToStatus(isLess, hasValues);
		}
	}
}
