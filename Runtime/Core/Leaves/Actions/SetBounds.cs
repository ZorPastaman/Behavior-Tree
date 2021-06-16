// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetBounds : Action,
		ISetupable<Vector3, Vector3, BlackboardPropertyName>, ISetupable<Vector3, Vector3, string>
	{
		[BehaviorInfo] private Vector3 m_center;
		[BehaviorInfo] private Vector3 m_size;
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<Vector3, Vector3, BlackboardPropertyName>.Setup(Vector3 center, Vector3 size,
			BlackboardPropertyName boundsPropertyName)
		{
			SetupInternal(center, size, boundsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<Vector3, Vector3, string>.Setup(Vector3 center, Vector3 size, string boundsPropertyName)
		{
			SetupInternal(center, size, new BlackboardPropertyName(boundsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(Vector3 center, Vector3 size, BlackboardPropertyName boundsPropertyName)
		{
			m_center = center;
			m_size = size;
			m_boundsPropertyName = boundsPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_boundsPropertyName, new Bounds(m_center, m_size));
			return Status.Success;
		}
	}
}
