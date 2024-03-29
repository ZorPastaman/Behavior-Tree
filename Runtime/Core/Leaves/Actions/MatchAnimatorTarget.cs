﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Matches an animator target. This <see cref="Action"/> uses
	/// <see cref="Animator.MatchTarget(Vector3, Quaternion, AvatarTarget, MatchTargetWeightMask, float)"/>
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
	/// 		<description>Property name of a position of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a rotation of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Avatar target of type <see cref="AvatarTarget"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Match target weight mask of type <see cref="MatchTargetWeightMask"/>.</description>
	/// 	</item>
	/// 		<item>
	/// 		<description>Property name of an avoided of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Start normalized time of type <see cref="float"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class MatchAnimatorTarget : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, AvatarTarget,
			MatchTargetWeightMask, float>,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, AvatarTarget,
			Vector3, float, float>,
		ISetupable<string, string, string, AvatarTarget, MatchTargetWeightMask, float>,
		ISetupable<string, string, string, AvatarTarget, Vector3, float, float>
	{
		[BehaviorInfo] private BlackboardPropertyName m_animatorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rotationPropertyName;
		[BehaviorInfo] private AvatarTarget m_avatarTarget;
		[BehaviorInfo] private MatchTargetWeightMask m_matchTargetWeightMask;
		[BehaviorInfo] private float m_startNormalizedTime;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, AvatarTarget,
			MatchTargetWeightMask, float>.Setup(BlackboardPropertyName animatorPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			AvatarTarget avatarTarget, MatchTargetWeightMask matchTargetWeightMask, float startNormalizedTime)
		{
			SetupInternal(animatorPropertyName, positionPropertyName, rotationPropertyName, avatarTarget,
				matchTargetWeightMask, startNormalizedTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, AvatarTarget,
			Vector3, float, float>.Setup(BlackboardPropertyName animatorPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			AvatarTarget avatarTarget, Vector3 positionXYZWeight, float rotationWeight, float startNormalizedTime)
		{
			SetupInternal(animatorPropertyName, positionPropertyName, rotationPropertyName, avatarTarget,
				new MatchTargetWeightMask(positionXYZWeight, rotationWeight), startNormalizedTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, AvatarTarget, MatchTargetWeightMask, float>.Setup(
			string animatorPropertyName, string positionPropertyName, string rotationPropertyName,
			AvatarTarget avatarTarget, MatchTargetWeightMask matchTargetWeightMask, float startNormalizedTime)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName),
				new BlackboardPropertyName(positionPropertyName), new BlackboardPropertyName(rotationPropertyName),
				avatarTarget, matchTargetWeightMask, startNormalizedTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, AvatarTarget, Vector3, float, float>.Setup(string animatorPropertyName,
			string positionPropertyName, string rotationPropertyName,
			AvatarTarget avatarTarget, Vector3 positionXYZWeight, float rotationWeight, float startNormalizedTime)
		{
			SetupInternal(new BlackboardPropertyName(animatorPropertyName),
				new BlackboardPropertyName(positionPropertyName), new BlackboardPropertyName(rotationPropertyName),
				avatarTarget, new MatchTargetWeightMask(positionXYZWeight, rotationWeight), startNormalizedTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName animatorPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			AvatarTarget avatarTarget, MatchTargetWeightMask matchTargetWeightMask, float startNormalizedTime)
		{
			m_animatorPropertyName = animatorPropertyName;
			m_positionPropertyName = positionPropertyName;
			m_rotationPropertyName = rotationPropertyName;
			m_avatarTarget = avatarTarget;
			m_matchTargetWeightMask = matchTargetWeightMask;
			m_startNormalizedTime = startNormalizedTime;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_animatorPropertyName, out Animator animator) & animator != null &
				blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position) &
				blackboard.TryGetStructValue(m_rotationPropertyName, out Quaternion rotation))
			{
				animator.MatchTarget(position, rotation, m_avatarTarget, m_matchTargetWeightMask,
					m_startNormalizedTime);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
