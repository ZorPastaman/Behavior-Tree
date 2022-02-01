// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a trigger into an <see cref="Animator"/>.
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
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetAnimatorTriggerProperty : Action,
		ISetupable<BlackboardPropertyName, int>, ISetupable<BlackboardPropertyName, string>,
		ISetupable<string, int>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private int m_propertyId;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int>.Setup(
			BlackboardPropertyName animatorPropertyName, int propertyId)
		{
			SetupInternal(animatorPropertyName, propertyId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string>.Setup(
			BlackboardPropertyName animatorPropertyName, string propertyName)
		{
			SetupInternal(animatorPropertyName, Animator.StringToHash(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int>.Setup(string animatorPropertyName, int propertyId)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), propertyId);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string animatorPropertyName, string propertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName), Animator.StringToHash(propertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName, int propertyId)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_propertyId = propertyId;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null)
			{
				animator.SetTrigger(m_propertyId);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
