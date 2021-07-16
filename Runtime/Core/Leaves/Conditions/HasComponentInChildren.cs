// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Conditions
{
	public sealed class HasComponentInChildren<T> : Condition, ISetupable<BlackboardPropertyName>, ISetupable<string>
		where T : Component
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName gameObjectPropertyName)
		{
			SetupInternal(gameObjectPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string gameObjectPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null)
			{
				return StateToStatusHelper.ConditionToStatus(gameObject.GetComponentInChildren<T>() != null);
			}

			return Status.Error;
		}
	}
}
