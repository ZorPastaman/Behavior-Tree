// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class MultiplyVector3AndNumber : Action,
		ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>, ISetupable<string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private float m_number;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName vectorPropertyName, float number, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(vectorPropertyName, number, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float, string>.Setup(string vectorPropertyName, float number, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), number,
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, float number,
			BlackboardPropertyName resultPropertyName)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_number = number;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_vectorPropertyName, out Vector3 vector))
			{
				blackboard.SetStructValue(m_resultPropertyName, vector * m_number);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
