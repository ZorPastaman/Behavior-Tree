// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class StopParticleSystem : Action,
		ISetupable<BlackboardPropertyName, bool, ParticleSystemStopBehavior>,
		ISetupable<string, bool, ParticleSystemStopBehavior>
	{
		[BehaviorInfo] private BlackboardPropertyName m_particleSystemPropertyName;
		[BehaviorInfo] private bool m_withChildren;
		[BehaviorInfo] private ParticleSystemStopBehavior m_stopBehavior;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool, ParticleSystemStopBehavior>.Setup(
			BlackboardPropertyName particleSystemPropertyName, bool withChildren,
			ParticleSystemStopBehavior stopBehavior)
		{
			SetupInternal(particleSystemPropertyName, withChildren, stopBehavior);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool, ParticleSystemStopBehavior>.Setup(string particleSystemPropertyName,
			bool withChildren, ParticleSystemStopBehavior stopBehavior)
		{
			SetupInternal(new BlackboardPropertyName(particleSystemPropertyName), withChildren, stopBehavior);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName particleSystemPropertyName, bool withChildren,
			ParticleSystemStopBehavior stopBehavior)
		{
			m_particleSystemPropertyName = particleSystemPropertyName;
			m_withChildren = withChildren;
			m_stopBehavior = stopBehavior;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_particleSystemPropertyName, out ParticleSystem particleSystem) &
				particleSystem != null)
			{
				particleSystem.Stop(m_withChildren, m_stopBehavior);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
