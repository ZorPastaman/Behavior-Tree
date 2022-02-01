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
	/// Checks if a <see cref="ParticleSystem"/> is stopped.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the <see cref="ParticleSystem"/> is stopped.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the <see cref="ParticleSystem"/> isn't stopped.</description>
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
	/// 		<description>Property name of a particle system of type <see cref="ParticleSystem"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsParticleSystemStopped : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_particleSystemPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName particleSystemPropertyName)
		{
			SetupInternal(particleSystemPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string particleSystemPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(particleSystemPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName particleSystemPropertyName)
		{
			m_particleSystemPropertyName = particleSystemPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_particleSystemPropertyName, out ParticleSystem particleSystem) &
				particleSystem != null)
			{
				return StateToStatusHelper.ConditionToStatus(particleSystem.isStopped);
			}

			return Status.Error;
		}
	}
}
