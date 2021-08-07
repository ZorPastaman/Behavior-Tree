// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetParticleSystemIsAlive : Action,
		ISetupable<BlackboardPropertyName, bool, BlackboardPropertyName>, ISetupable<string, bool, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_particleSystemPropertyName;
		[BehaviorInfo] private bool m_withChildren;
		[BehaviorInfo] private BlackboardPropertyName m_isAlivePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool, BlackboardPropertyName>.Setup(
			BlackboardPropertyName particleSystemPropertyName, bool withChildren,
			BlackboardPropertyName isAlivePropertyName)
		{
			SetupInternal(particleSystemPropertyName, withChildren, isAlivePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool, string>.Setup(string particleSystemPropertyName, bool withChildren,
			string isAlivePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(particleSystemPropertyName), withChildren,
				new BlackboardPropertyName(isAlivePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName particleSystemPropertyName, bool withChildren,
			BlackboardPropertyName isAlivePropertyName)
		{
			m_particleSystemPropertyName = particleSystemPropertyName;
			m_withChildren = withChildren;
			m_isAlivePropertyName = isAlivePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_particleSystemPropertyName, out ParticleSystem particleSystem) &
				particleSystem != null)
			{
				blackboard.SetStructValue(m_isAlivePropertyName, particleSystem.IsAlive(m_withChildren));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
