// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class LayerMaskIntersects : Condition,
		ISetupable<BlackboardPropertyName, LayerMask>, ISetupable<string, LayerMask>
	{
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;
		[BehaviorInfo] private LayerMask m_mask;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, LayerMask>.Setup(BlackboardPropertyName valuePropertyName,
			LayerMask mask)
		{
			SetupInternal(valuePropertyName, mask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, LayerMask>.Setup(string valuePropertyName, LayerMask mask)
		{
			SetupInternal(new BlackboardPropertyName(valuePropertyName), mask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName valuePropertyName, LayerMask mask)
		{
			m_valuePropertyName = valuePropertyName;
			m_mask = mask;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValue = blackboard.TryGetStructValue(m_valuePropertyName, out LayerMask value);
			return StateToStatusHelper.ConditionToStatus((value & m_mask) != 0, hasValue);
		}
	}
}
