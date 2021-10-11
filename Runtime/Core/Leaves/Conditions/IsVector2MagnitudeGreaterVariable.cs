// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class IsVector2MagnitudeGreaterVariable : Condition,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_magnitudePropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName vectorPropertyName,
			BlackboardPropertyName magnitudePropertyName)
		{
			SetupInternal(vectorPropertyName, magnitudePropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string vectorPropertyName, string magnitudePropertyName)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName),
				new BlackboardPropertyName(magnitudePropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName,
			BlackboardPropertyName magnitudePropertyName)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_magnitudePropertyName = magnitudePropertyName;
		}

		[Pure]
		protected override Status Execute()
		{
			bool hasValues = blackboard.TryGetStructValue(m_vectorPropertyName, out Vector2 vector) &
				blackboard.TryGetStructValue(m_magnitudePropertyName, out float magnitude);
			return StateToStatusHelper.ConditionToStatus(vector.sqrMagnitude > magnitude * magnitude, hasValues);
		}
	}
}
