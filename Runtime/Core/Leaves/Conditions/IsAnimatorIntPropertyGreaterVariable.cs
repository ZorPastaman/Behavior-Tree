// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if an animator <see cref="int"/> property is greater than a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the property is greater than the specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the property isn't greater than the specified value.</description>
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
	/// 		<description>Property name of an animator of type <see cref="Animator"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Property id or property name of type <see cref="int"/> or <see cref="string"/>.
	/// 		</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a value of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsAnimatorIntPropertyGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, int, BlackboardPropertyName>,
		ISetupable<BlackboardPropertyName, string, string>,
		ISetupable<string, int, BlackboardPropertyName>, ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_propertyId;
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int, BlackboardPropertyName>.Setup(
			BlackboardPropertyName animatorPropertyName, int propertyId, BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(animatorPropertyName, propertyId, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string, string>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName, string valuePropertyName)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName),
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int, BlackboardPropertyName>.Setup(string animatorPropertyName, int propertyId,
			BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string animatorPropertyName, string propertyName, string valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(propertyName),
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int propertyId,
			BlackboardPropertyName valuePropertyName)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_propertyId = propertyId;
			m_valuePropertyName = valuePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			return blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null &
				blackboard.TryGetStructValue(m_valuePropertyName, out int value)
					? StateToStatusHelper.ConditionToStatus(animator.GetInteger(m_propertyId) > value)
					: Status.Error;
		}
	}
}
