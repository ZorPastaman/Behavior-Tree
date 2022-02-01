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
	/// Checks if a <see cref="Collider"/> is enabled.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the <see cref="Collider"/> is enabled.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the <see cref="Collider"/> isn't enabled.</description>
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
	/// 		<description>Property name of a behavior of type <see cref="Collider"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsColliderEnabled : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colliderPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName colliderPropertyName)
		{
			SetupInternal(colliderPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string colliderPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colliderPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colliderPropertyName)
		{
			m_colliderPropertyName = colliderPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_colliderPropertyName, out Collider collider) & collider != null)
			{
				return StateToStatusHelper.ConditionToStatus(collider.enabled);
			}

			return Status.Error;
		}
	}
}
