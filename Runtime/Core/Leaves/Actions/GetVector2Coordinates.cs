// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetVector2Coordinates : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_xPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_yPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName vectorPropertyName, BlackboardPropertyName xPropertyName,
			BlackboardPropertyName yPropertyName)
		{
			SetupInternal(vectorPropertyName, xPropertyName, yPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string vectorPropertyName, string xPropertyName,
			string yPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(vectorPropertyName), new BlackboardPropertyName(xPropertyName),
				new BlackboardPropertyName(yPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName vectorPropertyName, BlackboardPropertyName xPropertyName,
			BlackboardPropertyName yPropertyName)
		{
			m_vectorPropertyName = vectorPropertyName;
			m_xPropertyName = xPropertyName;
			m_yPropertyName = yPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_vectorPropertyName, out Vector2 vector))
			{
				blackboard.SetStructValue(m_xPropertyName, vector.x);
				blackboard.SetStructValue(m_yPropertyName, vector.y);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
