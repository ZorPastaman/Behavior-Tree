// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetColor : Action,
		ISetupable<float, float, float, float, BlackboardPropertyName>, ISetupable<float, float, float, float, string>
	{
		[BehaviorInfo] private float m_red;
		[BehaviorInfo] private float m_green;
		[BehaviorInfo] private float m_blue;
		[BehaviorInfo] private float m_alpha;
		[BehaviorInfo] private BlackboardPropertyName m_colorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<float, float, float, float, BlackboardPropertyName>.Setup(
			float red, float green, float blue, float alpha, BlackboardPropertyName colorPropertyName)
		{
			SetupInternal(red, green, blue, alpha, colorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<float, float, float, float, string>.Setup(float red, float green, float blue, float alpha,
			string colorPropertyName)
		{
			SetupInternal(red, green, blue, alpha, new BlackboardPropertyName(colorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(float red, float green, float blue, float alpha,
			BlackboardPropertyName colorPropertyName)
		{
			m_colorPropertyName = colorPropertyName;
			m_red = red;
			m_green = green;
			m_blue = blue;
			m_alpha = alpha;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_colorPropertyName, new Color(m_red, m_green, m_blue, m_alpha));
			return Status.Success;
		}
	}
}
