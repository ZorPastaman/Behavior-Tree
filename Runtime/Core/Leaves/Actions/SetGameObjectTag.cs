// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetGameObjectTag : Action,
		ISetupable<BlackboardPropertyName, string>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_gameObjectPropertyName;
		[BehaviorInfo] private string m_tag;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, string>.Setup(BlackboardPropertyName gameObjectPropertyName, string tag)
		{
			SetupInternal(gameObjectPropertyName, tag);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string gameObjectPropertyName, string tag)
		{
			SetupInternal(new BlackboardPropertyName(gameObjectPropertyName), tag);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName gameObjectPropertyName, string tag)
		{
			m_gameObjectPropertyName = gameObjectPropertyName;
			m_tag = tag;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_gameObjectPropertyName, out GameObject gameObject) & gameObject != null)
			{
				gameObject.tag = m_tag;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
