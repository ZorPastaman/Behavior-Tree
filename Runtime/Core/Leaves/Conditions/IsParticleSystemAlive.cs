// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	/// <summary>
	/// <para>
	/// Checks if a <see cref="ParticleSystem"/> is alive.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the <see cref="ParticleSystem"/> is alive.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the <see cref="ParticleSystem"/> isn't alive.</description>
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
	/// 	<item>
	/// 		<description>WithChildren argument of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
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
