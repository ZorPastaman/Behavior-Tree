﻿// Copyright (c) 2020-2021 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	public sealed class CalculateNavMeshPathVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_sourcePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_targetPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_areaMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pathPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName areaMaskPropertyName, BlackboardPropertyName pathPropertyName)
		{
			SetupInternal(sourcePropertyName, targetPropertyName, areaMaskPropertyName, pathPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string sourcePropertyName, string targetPropertyName,
			string areaMaskPropertyName, string pathPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(sourcePropertyName),
				new BlackboardPropertyName(targetPropertyName), new BlackboardPropertyName(areaMaskPropertyName),
				new BlackboardPropertyName(pathPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName areaMaskPropertyName, BlackboardPropertyName pathPropertyName)
		{
			m_sourcePropertyName = sourcePropertyName;
			m_targetPropertyName = targetPropertyName;
			m_areaMaskPropertyName = areaMaskPropertyName;
			m_pathPropertyName = pathPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_sourcePropertyName, out Vector3 source) &
				blackboard.TryGetStructValue(m_targetPropertyName, out Vector3 target) &
				blackboard.TryGetStructValue(m_areaMaskPropertyName, out int areaMask))
			{
				if (!blackboard.TryGetClassValue(m_pathPropertyName, out NavMeshPath path) || path == null)
				{
					path = new NavMeshPath();
					blackboard.SetClassValue(m_pathPropertyName, path);
				}

				bool found = NavMesh.CalculatePath(source, target, areaMask, path);

				return StateToStatusHelper.ConditionToStatus(found);
			}

			return Status.Error;
		}
	}
}
