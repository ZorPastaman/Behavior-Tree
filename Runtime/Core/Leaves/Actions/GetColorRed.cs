// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetColorRed : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_redPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colorPropertyName, BlackboardPropertyName redPropertyName)
		{
			SetupInternal(colorPropertyName, redPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string colorPropertyName, string redPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colorPropertyName), new BlackboardPropertyName(redPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colorPropertyName, BlackboardPropertyName redPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_redPropertyName = redPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_colorPropertyName, out Color color))
			{
				blackboard.SetStructValue(m_redPropertyName, color.r);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
