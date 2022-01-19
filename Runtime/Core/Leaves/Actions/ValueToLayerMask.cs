// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class ValueToLayerMask : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_valuePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_layerMaskPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName valuePropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			SetupInternal(valuePropertyName, layerMaskPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string valuePropertyName, string layerMaskPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(valuePropertyName),
				new BlackboardPropertyName(layerMaskPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName valuePropertyName,
			BlackboardPropertyName layerMaskPropertyName)
		{
			m_valuePropertyName = valuePropertyName;
			m_layerMaskPropertyName = layerMaskPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_valuePropertyName, out int value))
			{
				LayerMask layerMask = value;
				blackboard.SetStructValue(m_layerMaskPropertyName, layerMask);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
