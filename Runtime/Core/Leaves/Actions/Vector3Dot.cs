// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Computes a dot for vectors and sets a result into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a first operand of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class Vector3Dot : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_fromPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_toPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_dotPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
			BlackboardPropertyName dotPropertyName)
		{
			SetupInternal(fromPropertyName, toPropertyName, dotPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string fromPropertyName, string toPropertyName,
			string dotPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(fromPropertyName), new BlackboardPropertyName(toPropertyName),
				new BlackboardPropertyName(dotPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName fromPropertyName, BlackboardPropertyName toPropertyName,
			BlackboardPropertyName dotPropertyName)
		{
			m_fromPropertyName = fromPropertyName;
			m_toPropertyName = toPropertyName;
			m_dotPropertyName = dotPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_fromPropertyName, out Vector3 from) &
				blackboard.TryGetStructValue(m_toPropertyName, out Vector3 to))
			{
				blackboard.SetStructValue(m_dotPropertyName, Vector3.Dot(from, to));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
