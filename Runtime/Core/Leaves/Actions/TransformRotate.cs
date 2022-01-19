// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class TransformRotate : Action, 
		ISetupable<BlackboardPropertyName, Vector3>, ISetupable<string, Vector3>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private Vector3 m_euler;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, Vector3>.Setup(BlackboardPropertyName transformPropertyName, 
			Vector3 euler)
		{
			SetupInternal(transformPropertyName, euler);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, Vector3>.Setup(string transformPropertyName, Vector3 euler)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName), euler);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName, Vector3 euler)
		{
			m_transformPropertyName = transformPropertyName;
			m_euler = euler;
		}
		
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null)
			{
				transform.Rotate(m_euler);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
