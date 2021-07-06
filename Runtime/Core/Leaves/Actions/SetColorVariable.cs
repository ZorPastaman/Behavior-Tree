// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetColorVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_redPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_greenPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_bluePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_alphaPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName redPropertyName, BlackboardPropertyName greenPropertyName,
			BlackboardPropertyName bluePropertyName, BlackboardPropertyName alphaPropertyName,
			BlackboardPropertyName colorPropertyName)
		{
			SetupInternal(redPropertyName, greenPropertyName, bluePropertyName, alphaPropertyName, colorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string redPropertyName, string greenPropertyName,
			string bluePropertyName, string alphaPropertyName, string colorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(redPropertyName), new BlackboardPropertyName(greenPropertyName),
				new BlackboardPropertyName(bluePropertyName), new BlackboardPropertyName(alphaPropertyName),
				new BlackboardPropertyName(colorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName redPropertyName, BlackboardPropertyName greenPropertyName,
			BlackboardPropertyName bluePropertyName, BlackboardPropertyName alphaPropertyName,
			BlackboardPropertyName colorPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_redPropertyName = redPropertyName;
			m_greenPropertyName = greenPropertyName;
			m_bluePropertyName = bluePropertyName;
			m_alphaPropertyName = alphaPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_redPropertyName, out float red) &
				blackboard.TryGetStructValue(m_greenPropertyName, out float green) &
				blackboard.TryGetStructValue(m_bluePropertyName, out float blue) &
				blackboard.TryGetStructValue(m_alphaPropertyName, out float alpha))
			{
				blackboard.SetStructValue(m_colorPropertyName, new Color(red, green, blue, alpha));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
