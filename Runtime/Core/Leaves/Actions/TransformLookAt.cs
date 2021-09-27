// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class TransformLookAt : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, Vector3>,
		ISetupable<string, string, Vector3>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_targetPropertyName;
		[BehaviorInfo] private Vector3 m_up;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, Vector3>.Setup(
			BlackboardPropertyName transformPropertyName, BlackboardPropertyName targetPropertyName, Vector3 up)
		{
			SetupInternal(transformPropertyName, targetPropertyName, up);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, Vector3>.Setup(string transformPropertyName, string targetPropertyName,
			Vector3 up)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName),
				new BlackboardPropertyName(targetPropertyName), up);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName,
			BlackboardPropertyName targetPropertyName, Vector3 up)
		{
			m_transformPropertyName = transformPropertyName;
			m_targetPropertyName = targetPropertyName;
			m_up = up;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null &
				blackboard.TryGetClassValue(m_targetPropertyName, out Transform target) & target != null)
			{
				transform.LookAt(target, m_up);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
