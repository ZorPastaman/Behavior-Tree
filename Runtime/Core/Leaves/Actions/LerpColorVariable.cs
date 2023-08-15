// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

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
	/// 		<description>Property name of a time operand of type <see cref="float"/>.</description>
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
	public sealed class LerpColorVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromColorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toColorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_timePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName fromColorPropertyName, BlackboardPropertyName toColorPropertyName,
			BlackboardPropertyName timePropertyName, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(fromColorPropertyName, toColorPropertyName, timePropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string fromColorPropertyName, string toColorPropertyName,
			string timePropertyName, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(fromColorPropertyName),
				new BlackboardPropertyName(toColorPropertyName), new BlackboardPropertyName(timePropertyName),
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromColorPropertyName,
			BlackboardPropertyName toColorPropertyName, BlackboardPropertyName timePropertyName, BlackboardPropertyName resultPropertyName)
		{
			m_fromColorPropertyName = fromColorPropertyName;
			m_toColorPropertyName = toColorPropertyName;
			m_timePropertyName = timePropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_fromColorPropertyName, out Color from) &
				blackboard.TryGetStructValue(m_toColorPropertyName, out Color to) &
				blackboard.TryGetStructValue(m_timePropertyName, out float time))
			{
				Color result = Color.Lerp(from, to, time);
				blackboard.SetStructValue(m_resultPropertyName, result);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
