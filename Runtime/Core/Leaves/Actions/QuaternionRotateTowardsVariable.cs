// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Rotates a from <see cref="Quaternion"/> to to <see cref="Quaternion"/>
	/// and sets a result into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a from of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a to of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of max degrees of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class QuaternionRotateTowardsVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maxDegreesDeltaPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
				BlackboardPropertyName maxDegreesDeltaPropertyName, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(fromPropertyName, toPropertyName, maxDegreesDeltaPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string fromPropertyName, string toPropertyName,
			string maxDegreesDeltaPropertyName, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(fromPropertyName), new BlackboardPropertyName(toPropertyName),
				new BlackboardPropertyName(maxDegreesDeltaPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
			BlackboardPropertyName maxDegreesDeltaPropertyName, BlackboardPropertyName resultPropertyName)
		{
			m_fromPropertyName = fromPropertyName;
			m_toPropertyName = toPropertyName;
			m_maxDegreesDeltaPropertyName = maxDegreesDeltaPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_fromPropertyName, out Quaternion from) &
				blackboard.TryGetStructValue(m_toPropertyName, out Quaternion to) &
				blackboard.TryGetStructValue(m_maxDegreesDeltaPropertyName, out float maxDegreesDelta))
			{
				blackboard.SetStructValue(m_resultPropertyName, Quaternion.RotateTowards(from, to, maxDegreesDelta));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
