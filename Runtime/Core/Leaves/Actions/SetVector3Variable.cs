// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetVector3Variable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_xPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_yPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_zPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_vectorPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName xPropertyName, BlackboardPropertyName yPropertyName,
			BlackboardPropertyName zPropertyName, BlackboardPropertyName vectorPropertyName)
		{
			SetupInternal(xPropertyName, yPropertyName, zPropertyName, vectorPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string xPropertyName, string yPropertyName,
			string zPropertyName, string vectorPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(xPropertyName), new BlackboardPropertyName(yPropertyName),
				new BlackboardPropertyName(zPropertyName), new BlackboardPropertyName(vectorPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName xPropertyName, BlackboardPropertyName yPropertyName,
			BlackboardPropertyName zPropertyName, BlackboardPropertyName vectorPropertyName)
		{
			m_xPropertyName = xPropertyName;
			m_yPropertyName = yPropertyName;
			m_zPropertyName = zPropertyName;
			m_vectorPropertyName = vectorPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_xPropertyName, out float x) &
				blackboard.TryGetStructValue(m_yPropertyName, out float y) &
				blackboard.TryGetStructValue(m_zPropertyName, out float z))
			{
				blackboard.SetStructValue(m_vectorPropertyName, new Vector3(x, y, z));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
