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
	/// Checks if a game object layer is equal to a specified value.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the game object layer is equal to a specified value.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the game object layer isn't equal to a specified value.</description>
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
	/// 		<description>Layer of type <see cref="int"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsGameObjectLayerEqual : Condition,
		ISetupable<BlackboardPropertyName, int>, ISetupable<string, int>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private int m_layer;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, int>.Setup(BlackboardPropertyName gameObjectPropertyName, int layer)
		{
			SetupInternal(gameObjectPropertyName, layer);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, int>.Setup(string gameObjectPropertyName, int layer)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName), layer);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName, int layer)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_layer = layer;
		}

		[Pure]
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null)
			{
				return StateToStatusHelper.ConditionToStatus(gameObject.layer == m_layer);
			}

			return Status.Error;
		}
	}
}
