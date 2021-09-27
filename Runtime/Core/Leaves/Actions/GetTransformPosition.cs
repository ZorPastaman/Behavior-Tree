// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetTransformPosition : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName transformPropertyName, BlackboardPropertyName positionPropertyName)
		{
			SetupInternal(transformPropertyName, positionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string transformPropertyName, string positionPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName),
				new BlackboardPropertyName(positionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName,
			BlackboardPropertyName positionPropertyName)
		{
			m_transformPropertyName = transformPropertyName;
			m_positionPropertyName = positionPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null)
			{
				blackboard.SetStructValue(m_positionPropertyName, transform.position);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
