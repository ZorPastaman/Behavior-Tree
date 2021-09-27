// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetTransformRotation : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rotationPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName transformPropertyName, BlackboardPropertyName rotationPropertyName)
		{
			SetupInternal(transformPropertyName, rotationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string transformPropertyName, string rotationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName),
				new BlackboardPropertyName(rotationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName,
			BlackboardPropertyName rotationPropertyName)
		{
			m_transformPropertyName = transformPropertyName;
			m_rotationPropertyName = rotationPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null)
			{
				blackboard.SetStructValue(m_rotationPropertyName, transform.rotation);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
