// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class LayerMaskToValue : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName layerMaskPropertyName, BlackboardPropertyName valuePropertyName)
		{
			SetupInternal(layerMaskPropertyName, valuePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string layerMaskPropertyName, string valuePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(layerMaskPropertyName),
				new BlackboardPropertyName(valuePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName layerMaskPropertyName,
			BlackboardPropertyName valuePropertyName)
		{
			m_layerMaskPropertyName = layerMaskPropertyName;
			m_valuePropertyName = valuePropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_layerMaskPropertyName, out LayerMask layerMask))
			{
				int value = layerMask.value;
				blackboard.SetStructValue(m_valuePropertyName, value);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
