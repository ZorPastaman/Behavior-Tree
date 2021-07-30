// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class InstantiateObject : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_prefabPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName prefabPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(prefabPropertyName, resultPropertyName);
		}

		void ISetupable<string, string>.Setup(string prefabPropertyName, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(prefabPropertyName),
				new BlackboardPropertyName(resultPropertyName));
		}

		private void SetupInternal(BlackboardPropertyName prefabPropertyName, BlackboardPropertyName resultPropertyName)
		{
			m_prefabPropertyName = prefabPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_prefabPropertyName, out Object prefab) & prefab != null)
			{
				blackboard.SetClassValue(m_resultPropertyName, Object.Instantiate(prefab));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
