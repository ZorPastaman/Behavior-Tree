// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetComponentTransform : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_componentPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName componentPropertyName, BlackboardPropertyName transformPropertyName)
		{
			SetupInternal(componentPropertyName, transformPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string componentPropertyName, string transformPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(componentPropertyName),
				new BlackboardPropertyName(transformPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName componentPropertyName,
			BlackboardPropertyName transformPropertyName)
		{
			m_componentPropertyName = componentPropertyName;
			m_transformPropertyName = transformPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_componentPropertyName, out Component component) & component != null)
			{
				blackboard.SetClassValue(m_transformPropertyName, component.transform);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
