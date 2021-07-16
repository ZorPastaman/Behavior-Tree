// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsGameObjectLayerInMask : Condition,
		ISetupable<BlackboardPropertyName, LayerMask>, ISetupable<string, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private LayerMask m_layerMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, LayerMask>.Setup(BlackboardPropertyName gameObjectPropertyName, LayerMask layer)
		{
			SetupInternal(gameObjectPropertyName, layer);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, LayerMask>.Setup(string gameObjectPropertyName, LayerMask layer)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName), layer);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName, LayerMask layer)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_layerMask = layer;
		}

		[Pure]
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null)
			{
				return StateToStatusHelper.ConditionToStatus(((1 << gameObject.layer) & m_layerMask) != 0);
			}

			return Status.Error;
		}
	}
}
