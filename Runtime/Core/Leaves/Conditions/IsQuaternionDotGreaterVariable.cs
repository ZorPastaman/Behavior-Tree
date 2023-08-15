// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a dot between quaternions is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the dot is greater than the specified dot.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the dot isn't greater than the specified dot.</description>
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
	/// 		<description>Property name of a first operand of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a dot of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsQuaternionDotGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_firstOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_secondOperandPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_dotPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName firstOperandPropertyName, BlackboardPropertyName secondOperandPropertyName,
			BlackboardPropertyName dotPropertyName)
		{
			SetupInternal(firstOperandPropertyName, secondOperandPropertyName, dotPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string firstOperandPropertyName, string secondOperandPropertyName,
			string dotPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(firstOperandPropertyName),
				new BlackboardPropertyName(secondOperandPropertyName), new BlackboardPropertyName(dotPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName firstOperandPropertyName,
			BlackboardPropertyName secondOperandPropertyName, BlackboardPropertyName dotPropertyName)
		{
			m_firstOperandPropertyName = firstOperandPropertyName;
			m_secondOperandPropertyName = secondOperandPropertyName;
			m_dotPropertyName = dotPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_firstOperandPropertyName, out Quaternion first) &
				blackboard.TryGetStructValue(m_secondOperandPropertyName, out Quaternion second) &
				blackboard.TryGetStructValue(m_dotPropertyName, out float dot);

			return StateToStatusHelper.ConditionToStatus(Quaternion.Dot(first, second) > dot, hasValues);
		}
	}
}
