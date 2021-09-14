// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class SetQuaternion : Action,
		ISetupable<float, float, float, float, BlackboardPropertyName>,
		ISetupable<float, float, float, float, string>
	{
		[BehaviorInfo] private float m_x;
		[BehaviorInfo] private float m_y;
		[BehaviorInfo] private float m_z;
		[BehaviorInfo] private float m_w;
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<float, float, float, float, BlackboardPropertyName>.Setup(float x, float y, float z, float w,
			BlackboardPropertyName quaternionPropertyName)
		{
			SetupInternal(x, y, z, w, quaternionPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<float, float, float, float, string>.Setup(float x, float y, float z, float w,
			string quaternionPropertyName)
		{
			SetupInternal(x, y, z, w, new BlackboardPropertyName(quaternionPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(float x, float y, float z, float w, BlackboardPropertyName quaternionPropertyName)
		{
			m_x = x;
			m_y = y;
			m_z = z;
			m_w = w;
			m_quaternionPropertyName = quaternionPropertyName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override Status Execute()
		{
			blackboard.SetStructValue(m_quaternionPropertyName, new Quaternion(m_x, m_y, m_z, m_w));
			return Status.Success;
		}
	}
}
