﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsParticleSystemAlive : Condition,
		ISetupable<BlackboardPropertyName, bool>, ISetupable<string, bool>
	{
		[BehaviorInfo] private BlackboardPropertyName m_particleSystemPropertyName;
		[BehaviorInfo] private bool m_withChildren;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool>.Setup(BlackboardPropertyName particleSystemPropertyName,
			bool withChildren)
		{
			SetupInternal(particleSystemPropertyName, withChildren);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool>.Setup(string particleSystemPropertyName, bool withChildren)
		{
			SetupInternal(new BlackboardPropertyName(particleSystemPropertyName), withChildren);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName particleSystemPropertyName, bool withChildren)
		{
			m_particleSystemPropertyName = particleSystemPropertyName;
			m_withChildren = withChildren;
		}

		[Pure]
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_particleSystemPropertyName, out ParticleSystem particleSystem) &
				particleSystem != null)
			{
				return StateToStatusHelper.ConditionToStatus(particleSystem.IsAlive(m_withChildren));
			}

			return Status.Error;
		}
	}
}
