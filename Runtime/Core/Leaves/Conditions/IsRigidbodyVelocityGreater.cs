// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a <see cref="Rigidbody"/> velocity is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the velocity is greater than the specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the velocity isn't greater than the specified value.</description>
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
	/// 		<description>Value of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsRigidbodyVelocityGreater : Condition,
		ISetupable<BlackboardPropertyName, float>, ISetupable<string, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;
		[BehaviorInfo] private float m_sqrVelocity;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float>.Setup(BlackboardPropertyName rigidbodyPropertyName,
			float velocity)
		{
			SetupInternal(rigidbodyPropertyName, velocity);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float>.Setup(string rigidbodyPropertyName, float velocity)
		{
			SetupInternal(new BlackboardPropertyName(rigidbodyPropertyName), velocity);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName rigidbodyPropertyName, float velocity)
		{
			m_rigidbodyPropertyName = rigidbodyPropertyName;
			m_sqrVelocity = velocity * velocity;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_rigidbodyPropertyName, out Rigidbody rigidbody) & rigidbody != null)
			{
				return StateToStatusHelper.ConditionToStatus(rigidbody.velocity.sqrMagnitude > m_sqrVelocity);
			}

			return Status.Error;
		}
	}
}
