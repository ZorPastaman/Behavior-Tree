// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetParticleSystemIsStopped : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_particleSystemPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_isStoppedPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName particleSystemPropertyName, BlackboardPropertyName isStoppedPropertyName)
		{
			SetupInternal(particleSystemPropertyName, isStoppedPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string particleSystemPropertyName, string isStoppedPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(particleSystemPropertyName),
				new BlackboardPropertyName(isStoppedPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName particleSystemPropertyName,
			BlackboardPropertyName isStoppedPropertyName)
		{
			m_particleSystemPropertyName = particleSystemPropertyName;
			m_isStoppedPropertyName = isStoppedPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_particleSystemPropertyName, out ParticleSystem particleSystem) &
				particleSystem != null)
			{
				blackboard.SetStructValue(m_isStoppedPropertyName, particleSystem.isStopped);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
