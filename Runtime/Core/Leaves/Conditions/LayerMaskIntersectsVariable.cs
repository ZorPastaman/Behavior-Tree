// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class LayerMaskIntersectsVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_maskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName valuePropertyName,
			BlackboardPropertyName maskPropertyName)
		{
			SetupInternal(valuePropertyName, maskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string valuePropertyName, string maskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(valuePropertyName), new BlackboardPropertyName(maskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName valuePropertyName, BlackboardPropertyName maskPropertyName)
		{
			m_valuePropertyName = valuePropertyName;
			m_maskPropertyName = maskPropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_valuePropertyName, out LayerMask value) &
				blackboard.TryGetStructValue(m_maskPropertyName, out LayerMask mask);
			return StateToStatusHelper.ConditionToStatus((value & mask) != 0, hasValues);
		}
	}
}
