// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetColorGreen : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_greenPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colorPropertyName, BlackboardPropertyName greenPropertyName)
		{
			SetupInternal(colorPropertyName, greenPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string colorPropertyName, string greenPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName), new BlackboardPropertyName(greenPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colorPropertyName, BlackboardPropertyName greenPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_greenPropertyName = greenPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_colorPropertyName, out Color color))
			{
				blackboard.SetStructValue(m_greenPropertyName, color.g);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
