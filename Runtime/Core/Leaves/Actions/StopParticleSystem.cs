// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Stops a <see cref="ParticleSystem"/>.
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
	/// 		<description>Property name of a particle system of type <see cref="ParticleSystem"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>WithChildren argument of type <see cref="bool"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>
	/// 		Property name of a stop behavior of type <see cref="ParticleSystemStopBehavior"/>.
	/// 		</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
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
