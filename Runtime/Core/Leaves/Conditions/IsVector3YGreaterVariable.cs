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
	/// Checks if a <see cref="Vector3.y"/> is less than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the vector y is less than the specified red.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the vector y isn't less than the specified red.</description>
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
	/// 		<description>Property name of a vector of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of y of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsVector3YGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_yPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName vectorPropertyName, BlackboardPropertyName yPropertyName)
		{
			SetupInternal(vectorPropertyName, yPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string vectorPropertyName, string yPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), new BlackboardPropertyName(yPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, BlackboardPropertyName yPropertyName)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_yPropertyName = yPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_vectorPropertyName, out Vector3 vector) &
				blackboard.TryGetStructValue(m_yPropertyName, out float y);

			return StateToStatusHelper.ConditionToStatus(vector.y > y, hasValues);
		}
	}
}
