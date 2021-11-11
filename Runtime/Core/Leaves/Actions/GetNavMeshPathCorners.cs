// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class GetNavMeshPathCorners : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_pathPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_cornersPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(BlackboardPropertyName pathPropertyName,
			BlackboardPropertyName cornersPropertyName)
		{
			SetupInternal(pathPropertyName, cornersPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string pathPropertyName, string cornersPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(pathPropertyName),
				new BlackboardPropertyName(cornersPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName pathPropertyName, BlackboardPropertyName cornersPropertyName)
		{
			m_pathPropertyName = pathPropertyName;
			m_cornersPropertyName = cornersPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_pathPropertyName, out NavMeshPath path) & path != null)
			{
				blackboard.SetClassValue(m_cornersPropertyName, path.corners);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
