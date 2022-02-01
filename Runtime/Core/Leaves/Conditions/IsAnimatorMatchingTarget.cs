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
	/// Checks if an <see cref="Animator"/> is matching target.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the <see cref="Animator"/> is matching target.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the <see cref="Animator"/> isn't matching target.</description>
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
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsAnimatorMatchingTarget : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName animatorPropertyName)
		{
			SetupInternal(animatorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string animatorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName)
		{
			m_animatorPropertyName = animatorPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			return blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null
				? StateToStatusHelper.ConditionToStatus(animator.isMatchingTarget)
				: Status.Error;
		}
	}
}
