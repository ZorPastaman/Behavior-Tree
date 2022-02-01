// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="GameObject"/> activity using <see cref="GameObject.SetActive"/>.
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
	/// 		<description>Property name of a game object of type <see cref="GameObject"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Active of type <see cref="bool"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetGameObjectActive : Action, ISetupable<BlackboardPropertyName, bool>, ISetupable<string, bool>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private bool m_active;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, bool>.Setup(BlackboardPropertyName gameObjectPropertyName, bool active)
		{
			SetupInternal(gameObjectPropertyName, active);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, bool>.Setup(string gameObjectPropertyName, bool active)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName), active);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName, bool active)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_active = active;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null)
			{
				gameObject.SetActive(m_active);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
