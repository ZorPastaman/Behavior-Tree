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
	/// Checks if a magnitude between vectors is less than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the magnitude is less than the specified magnitude.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the magnitude isn't less than the specified magnitude.</description>
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
	/// 		<description>Property name of a first operand of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a second operand of type <see cref="Vector2"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Magnitude of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsVector2MagnitudeLess : Condition,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private float m_sqrMagnitude;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName vectorPropertyName, float magnitude)
		{
			SetupInternal(vectorPropertyName, magnitude);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string vectorPropertyName, float magnitude)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), magnitude);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, float magnitude)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_sqrMagnitude = magnitude * magnitude;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_vectorPropertyName, out Vector2 vector);
			return StateToStatusHelper.ConditionToStatus(vector.sqrMagnitude < m_sqrMagnitude, hasValue);
		}
	}
}
