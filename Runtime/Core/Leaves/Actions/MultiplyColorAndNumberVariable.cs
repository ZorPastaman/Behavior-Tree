// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class MultiplyColorAndNumberVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_numberPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colorPropertyName, BlackboardPropertyName numberPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(colorPropertyName, numberPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string colorPropertyName, string numberPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName),
				new BlackboardPropertyName(numberPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colorPropertyName,
			BlackboardPropertyName numberPropertyName, BlackboardPropertyName resultPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_numberPropertyName = numberPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_colorPropertyName, out Color color) &
				blackboard.TryGetStructValue(m_numberPropertyName, out float number))
			{
				Color result = color * number;
				blackboard.SetStructValue(m_resultPropertyName, result);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
