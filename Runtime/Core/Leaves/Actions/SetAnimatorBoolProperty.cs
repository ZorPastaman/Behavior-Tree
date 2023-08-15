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
	/// 		<description>Value of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetAnimatorBoolProperty : Action,
		ISetupable<BlackboardPropertyName, int, bool>, ISetupable<BlackboardPropertyName, string, bool>,
		ISetupable<string, int, bool>, ISetupable<string, string, bool>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_propertyId;
		[BehaviorInfo] private bool m_value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int, bool>.Setup(
			BlackboardPropertyName animatorPropertyName, int propertyId, bool value)
		{
			SetupInternal(animatorPropertyName, propertyId, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string, bool>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName, bool value)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName), value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int, bool>.Setup(string animatorPropertyName, int propertyId, bool value)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, bool>.Setup(string animatorPropertyName, string propertyName, bool value)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(propertyName), value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int propertyId, bool value)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_propertyId = propertyId;
			m_value = value;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				animator.SetBool(m_propertyId, m_value);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
