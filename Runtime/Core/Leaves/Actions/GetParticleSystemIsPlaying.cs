// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetParticleSystemIsPlaying : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_particleSystemPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_isPlayingPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName particleSystemPropertyName, BlackboardPropertyName isPlayingPropertyName)
		{
			SetupInternal(particleSystemPropertyName, isPlayingPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string particleSystemPropertyName, string isPlayingPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(particleSystemPropertyName),
				new BlackboardPropertyName(isPlayingPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName particleSystemPropertyName,
			BlackboardPropertyName isPlayingPropertyName)
		{
			m_particleSystemPropertyName = particleSystemPropertyName;
			m_isPlayingPropertyName = isPlayingPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_particleSystemPropertyName, out ParticleSystem particleSystem) &
				particleSystem != null)
			{
				blackboard.SetStructValue(m_isPlayingPropertyName, particleSystem.isPlaying);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
