// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class InstantiateObjectInPosition : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_prefabPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rotationPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.
			Setup(BlackboardPropertyName prefabPropertyName, BlackboardPropertyName positionPropertyName,
				BlackboardPropertyName rotationPropertyName, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(prefabPropertyName, positionPropertyName, rotationPropertyName, resultPropertyName);
		}

		void ISetupable<string, string, string, string>.Setup(string prefabPropertyName, string positionPropertyName,
			string rotationPropertyName, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(prefabPropertyName),
				new BlackboardPropertyName(positionPropertyName), new BlackboardPropertyName(rotationPropertyName),
				new BlackboardPropertyName(resultPropertyName));
		}

		private void SetupInternal(BlackboardPropertyName prefabPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_prefabPropertyName = prefabPropertyName;
			m_positionPropertyName = positionPropertyName;
			m_rotationPropertyName = rotationPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_prefabPropertyName, out Object prefab) & prefab != null &
				blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position) &
				blackboard.TryGetStructValue(m_rotationPropertyName, out Quaternion rotation))
			{
				blackboard.SetClassValue(m_resultPropertyName, Object.Instantiate(prefab, position, rotation));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
