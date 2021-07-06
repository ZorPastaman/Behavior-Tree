// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetColorBlue : Action,
		ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>, ISetupable<string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private float m_blue;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colorPropertyName, float blue, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(colorPropertyName, blue, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float, string>.Setup(string colorPropertyName, float blue, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName), blue,
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colorPropertyName, float blue,
			BlackboardPropertyName resultPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_blue = blue;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_colorPropertyName, out Color color))
			{
				color.b = m_blue;
				blackboard.SetStructValue(m_resultPropertyName, color);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
