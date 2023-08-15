﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Raycast a nav mesh and sets a computed <see cref="NavMeshHit"/> into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the hit exists.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the hit doesn't exist.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Error"/> </term>
	/// 		<description>if there's no data in the <see cref="Blackboard"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// <para>
	/// <list type="number">
	/// 	<listheader>
	/// 		<term>Setup arguments:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<description>Property name of a source of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a target of type <see cref="Vector3"/>.</description>
	/// 	</item>
	///  	<item>
	/// 		<description>Property name of an area mask of type <see cref="int"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="NavMeshHit"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class RaycastNavMeshVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_sourcePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_targetPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_areaMaskPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_hitPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName areaMaskPropertyName, BlackboardPropertyName hitPropertyName)
		{
			SetupInternal(sourcePropertyName, targetPropertyName, areaMaskPropertyName, hitPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string sourcePropertyName, string targetPropertyName,
			string areaMaskPropertyName, string hitPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(sourcePropertyName),
				new BlackboardPropertyName(targetPropertyName), new BlackboardPropertyName(areaMaskPropertyName),
				new BlackboardPropertyName(hitPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName areaMaskPropertyName, BlackboardPropertyName hitPropertyName)
		{
			m_sourcePropertyName = sourcePropertyName;
			m_targetPropertyName = targetPropertyName;
			m_areaMaskPropertyName = areaMaskPropertyName;
			m_hitPropertyName = hitPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_sourcePropertyName, out Vector3 source) &
				blackboard.TryGetStructValue(m_targetPropertyName, out Vector3 target) &
				blackboard.TryGetStructValue(m_areaMaskPropertyName, out int areaMask))
			{
				if (NavMesh.Raycast(source, target, out NavMeshHit hit, areaMask))
				{
					blackboard.SetStructValue(m_hitPropertyName, hit);
					return Status.Success;
				}

				return Status.Failure;
			}

			return Status.Error;
		}
	}
}
