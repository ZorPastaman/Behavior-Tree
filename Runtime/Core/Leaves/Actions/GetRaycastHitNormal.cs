// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetRaycastHitNormal : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_raycastHitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_normalPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName raycastHitPropertyName, BlackboardPropertyName normalPropertyName)
		{
			SetupInternal(raycastHitPropertyName, normalPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string raycastHitPropertyName, string normalPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(raycastHitPropertyName),
				new BlackboardPropertyName(normalPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName raycastHitPropertyName,
			BlackboardPropertyName normalPropertyName)
		{
			m_raycastHitPropertyName = raycastHitPropertyName;
			m_normalPropertyName = normalPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_raycastHitPropertyName, out RaycastHit raycastHit))
			{
				blackboard.SetStructValue(m_normalPropertyName, raycastHit.normal);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
