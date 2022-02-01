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
	/// 		<description>Y of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsVector3YLess : Condition, ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private float m_y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName vectorPropertyName, float y)
		{
			SetupInternal(vectorPropertyName, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string vectorPropertyName, float y)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, float y)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_y = y;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_vectorPropertyName, out Vector3 vector);
			return StateToStatusHelper.ConditionToStatus(vector.y < m_y, hasValue);
		}
	}
}
