// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a <see cref="Rigidbody"/> velocity is less than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the velocity is less than the specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the velocity isn't less than the specified value.</description>
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
	/// 		<description>Property name of a rigidbody of type <see cref="Rigidbody"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a value of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsRigidbodyVelocityLessVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_velocityPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName rigidbodyPropertyName, BlackboardPropertyName velocityPropertyName)
		{
			SetupInternal(rigidbodyPropertyName, velocityPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string rigidbodyPropertyName, string velocityPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName),
				new BlackboardPropertyName(velocityPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName,
			BlackboardPropertyName velocityPropertyName)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_velocityPropertyName = velocityPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null &
				blackboard.TryGetStructValue(m_velocityPropertyName, out float velocity))
			{
				return StateToStatusHelper.ConditionToStatus(rigidbody.velocity.sqrMagnitude < velocity * velocity);
			}

			return Status.Error;
		}
	}
}
