// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetVector2X : Action,
		ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>, ISetupable<string, float, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private float m_x;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, float, BlackboardPropertyName>.Setup(
			BlackboardPropertyName vectorPropertyName, float x, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(vectorPropertyName, x, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, float, string>.Setup(string vectorPropertyName, float x,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), x,
				new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, float x,
			BlackboardPropertyName resultPropertyName)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_x = x;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_vectorPropertyName, out Vector2 vector))
			{
				vector.x = m_x;
				blackboard.SetStructValue(m_resultPropertyName, vector);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
