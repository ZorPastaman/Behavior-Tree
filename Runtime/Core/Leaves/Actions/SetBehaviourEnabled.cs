// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="Behaviour.enabled"/>.
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
	/// 		<description>Property name of a behaviour of type <see cref="Behaviour"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Enabled of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetBehaviourEnabled : Action, ISetupable<BlackboardPropertyName, bool>, ISetupable<string, bool>
	{
		[BehaviorInfo] private BlackboardPropertyName m_behaviourPropertyName;
		[BehaviorInfo] private bool m_enabled;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool>.Setup(BlackboardPropertyName behaviourPropertyName, bool enabled)
		{
			SetupInternal(behaviourPropertyName, enabled);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool>.Setup(string behaviourPropertyName, bool enabled)
		{
			SetupInternal(new BlackboardPropertyName(behaviourPropertyName), enabled);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName behaviourPropertyName, bool enabled)
		{
			m_behaviourPropertyName = behaviourPropertyName;
			m_enabled = enabled;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_behaviourPropertyName, out Behaviour behaviour) & behaviour != null)
			{
				behaviour.enabled = m_enabled;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
