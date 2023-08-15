// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets a <see cref="ParticleSystem.IsAlive(bool)"/> and sets it into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name for a result of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
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
