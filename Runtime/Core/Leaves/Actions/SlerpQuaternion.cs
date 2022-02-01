// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Slerps quaternions and sets the result into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a first operand of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Time operand of type <see cref="float"/>.</description>
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
	public sealed class SlerpQuaternion : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>,
		ISetupable<string, string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toPropertyName;
		[BehaviorInfo] private float m_time;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName, float time,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(fromPropertyName, toPropertyName, time, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float, string>.Setup(string fromPropertyName, string toPropertyName, float time,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(fromPropertyName), new BlackboardPropertyName(toPropertyName),
				time, new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
			float time, BlackboardPropertyName resultPropertyName)
		{
			m_fromPropertyName = fromPropertyName;
			m_toPropertyName = toPropertyName;
			m_time = time;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_fromPropertyName, out Quaternion from) &
				blackboard.TryGetStructValue(m_toPropertyName, out Quaternion to))
			{
				blackboard.SetStructValue(m_resultPropertyName, Quaternion.Slerp(from, to, m_time));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
