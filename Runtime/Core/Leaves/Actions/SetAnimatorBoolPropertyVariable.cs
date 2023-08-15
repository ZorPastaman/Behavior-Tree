// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a bool property into an <see cref="Animator"/>.
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
	/// 		<description>Property name of an animator of type <see cref="Animator"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Property name or property id of type <see cref="string"/> or <see cref="int"/>.
	/// 		</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a value of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetAnimatorBoolPropertyVariable : Action,
		ISetupable<BlackboardPropertyName, int, BlackboardPropertyName>,
		ISetupable<BlackboardPropertyName, string, BlackboardPropertyName>,
		ISetupable<string, int, string>, ISetupable<string, string, string>
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
		void ISetupable<BlackboardPropertyName, string, BlackboardPropertyName>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName, BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName), valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int, string>.Setup(
			string animatorPropertyName, int propertyId, string valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId,
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(
			string animatorPropertyName, string propertyName, string valuePropertyName)
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

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_valuePropertyName, out bool value) &
				blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				animator.SetBool(m_propertyId, value);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
