// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Lerps colors and sets the result into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a first operand of type <see cref="Color"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Color"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Time operand of type <see cref="float"/>.</description>
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
	public sealed class LerpColor : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>,
		ISetupable<string, string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromColorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toColorPropertyName;
		[BehaviorInfo] private float m_time;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName fromColorPropertyName, BlackboardPropertyName toColorPropertyName, float time,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(fromColorPropertyName, toColorPropertyName, time, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, float, string>.Setup(string fromColorPropertyName, string toColorPropertyName,
			float time, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(fromColorPropertyName),
				new BlackboardPropertyName(toColorPropertyName), time, new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromColorPropertyName,
			BlackboardPropertyName toColorPropertyName, float time, BlackboardPropertyName resultPropertyName)
		{
			m_fromColorPropertyName = fromColorPropertyName;
			m_toColorPropertyName = toColorPropertyName;
			m_time = time;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_fromColorPropertyName, out Color from) &
				blackboard.TryGetStructValue(m_toColorPropertyName, out Color to))
			{
				Color result = Color.Lerp(from, to, m_time);
				blackboard.SetStructValue(m_resultPropertyName, result);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
