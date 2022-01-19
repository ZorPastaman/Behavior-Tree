// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class TransformLookAtVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_targetPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_upPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName transformPropertyName, BlackboardPropertyName targetPropertyName, 
			BlackboardPropertyName upPropertyName)
		{
			SetupInternal(transformPropertyName, targetPropertyName, upPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string transformPropertyName, string targetPropertyName,
			string upPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName),
				new BlackboardPropertyName(targetPropertyName), new BlackboardPropertyName(upPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName,
			BlackboardPropertyName targetPropertyName, BlackboardPropertyName upPropertyName)
		{
			m_transformPropertyName = transformPropertyName;
			m_targetPropertyName = targetPropertyName;
			m_upPropertyName = upPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null &
				blackboard.TryGetClassValue(m_targetPropertyName, out Transform target) & target != null &
				blackboard.TryGetStructValue(m_upPropertyName, out Vector3 up))
			{
				transform.LookAt(target, up);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
