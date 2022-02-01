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
	/// Checks if a game object layer is in a specified layer mask.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the game object layer is in a specified layer mask.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the game object layer isn't in a specified layer mask.</description>
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
	/// 		<description>Property name of a layer mask of type <see cref="LayerMask"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class IsGameObjectLayerInMaskVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName gameObjectPropertyName, BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(gameObjectPropertyName, layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string gameObjectPropertyName, string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName),
				new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null &
				blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask))
			{
				return StateToStatusHelper.ConditionToStatus(((1 << gameObject.layer) & layerMask) != 0);
			}

			return Status.Error;
		}
	}
}
