// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class InstantiateObjectInParent : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_prefabPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_parentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName prefabPropertyName, BlackboardPropertyName parentPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(prefabPropertyName, parentPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string prefabPropertyName, string parentPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(prefabPropertyName),
				new BlackboardPropertyName(parentPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName prefabPropertyName, BlackboardPropertyName parentPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_prefabPropertyName = prefabPropertyName;
			m_parentPropertyName = parentPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_prefabPropertyName, out Object prefab) & prefab != null &
				blackboard.TryGetClassValue(m_parentPropertyName, out Transform parent) & parent != null)
			{
				blackboard.SetClassValue(m_resultPropertyName, Object.Instantiate(prefab, parent));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
