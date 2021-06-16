// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetBoundsVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_centerPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_sizePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName centerPropertyName, BlackboardPropertyName sizePropertyName,
			BlackboardPropertyName boundsPropertyName)
		{
			SetupInternal(centerPropertyName, sizePropertyName, boundsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string centerPropertyName, string sizePropertyName,
			string boundsPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(centerPropertyName), new BlackboardPropertyName(sizePropertyName),
				new BlackboardPropertyName(boundsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName centerPropertyName, BlackboardPropertyName sizePropertyName,
			BlackboardPropertyName boundsPropertyName)
		{
			m_centerPropertyName = centerPropertyName;
			m_sizePropertyName = sizePropertyName;
			m_boundsPropertyName = boundsPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_centerPropertyName, out Vector3 center) &
				blackboard.TryGetStructValue(m_sizePropertyName, out Vector3 size))
			{
				blackboard.SetStructValue(m_boundsPropertyName, new Bounds(center, size));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
