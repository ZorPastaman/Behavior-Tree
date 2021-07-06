// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetComponentGameObject : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_componentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName componentPropertyName, BlackboardPropertyName gameObjectPropertyName)
		{
			SetupInternal(componentPropertyName, gameObjectPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string componentPropertyName, string gameObjectPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(componentPropertyName),
				new BlackboardPropertyName(gameObjectPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName componentPropertyName,
			BlackboardPropertyName gameObjectPropertyName)
		{
			m_componentPropertyName = componentPropertyName;
			m_gameObjectPropertyName = gameObjectPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_componentPropertyName, out Component component) & component != null)
			{
				blackboard.SetClassValue(m_gameObjectPropertyName, component.gameObject);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
