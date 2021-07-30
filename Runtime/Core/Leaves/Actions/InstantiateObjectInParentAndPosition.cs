// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class InstantiateObjectInParentAndPosition : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>,
		ISetupable<string, string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_prefabPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_parentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rotationPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName,
			BlackboardPropertyName>.Setup(
			BlackboardPropertyName prefabPropertyName, BlackboardPropertyName parentPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(prefabPropertyName, parentPropertyName, positionPropertyName, rotationPropertyName,
				resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string, string>.Setup(string prefabPropertyName,
			string parentPropertyName, string positionPropertyName, string rotationPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(prefabPropertyName),
				new BlackboardPropertyName(parentPropertyName), new BlackboardPropertyName(positionPropertyName),
				new BlackboardPropertyName(rotationPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName prefabPropertyName, BlackboardPropertyName parentPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_prefabPropertyName = prefabPropertyName;
			m_parentPropertyName = parentPropertyName;
			m_positionPropertyName = positionPropertyName;
			m_rotationPropertyName = rotationPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_prefabPropertyName, out Object prefab) & prefab != null &
				blackboard.TryGetClassValue(m_parentPropertyName, out Transform parent) & parent != null &
				blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position) &
				blackboard.TryGetStructValue(m_rotationPropertyName, out Quaternion rotation))
			{
				blackboard.SetClassValue(m_resultPropertyName, Object.Instantiate(prefab, position, rotation, parent));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
