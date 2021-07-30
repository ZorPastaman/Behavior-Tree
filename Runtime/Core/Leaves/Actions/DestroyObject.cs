// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class DestroyObject : Action, ISetupable<BlackboardPropertyName>, ISetupable<string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_objectPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName>.Setup(BlackboardPropertyName objectPropertyName)
		{
			SetupInternal(objectPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string>.Setup(string objectPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(objectPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName objectPropertyName)
		{
			m_objectPropertyName = objectPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_objectPropertyName, out Object objectToDestroy) & objectToDestroy != null)
			{
				Object.Destroy(objectToDestroy);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
