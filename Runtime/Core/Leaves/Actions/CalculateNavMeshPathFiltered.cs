// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Calculates a nav mesh path and set it to the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if the path is found.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term><see cref="Status.Failure"/> </term>
	/// 		<description>if the path isn't found.</description>
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
	/// 		<description>Property name of a source position of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a target position of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a filter of type <see cref="NavMeshQueryFilter"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a nav mesh path of type <see cref="NavMeshPath"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/> or <see cref="Status.Failure"/>.
	/// But the path is empty if this <see cref="Action"/> ticks with <see cref="Status.Failure"/>.
	/// </remarks>
	public sealed class CalculateNavMeshPathFiltered : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_sourcePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_targetPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_filterPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pathPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>
			.Setup(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName filterPropertyName, BlackboardPropertyName pathPropertyName)
		{
			SetupInternal(sourcePropertyName, targetPropertyName, filterPropertyName, pathPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string, string>.Setup(string sourcePropertyName, string targetPropertyName,
			string filterPropertyName, string pathPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(sourcePropertyName),
				new BlackboardPropertyName(targetPropertyName), new BlackboardPropertyName(filterPropertyName),
				new BlackboardPropertyName(pathPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName sourcePropertyName, BlackboardPropertyName targetPropertyName,
			BlackboardPropertyName filterPropertyName, BlackboardPropertyName pathPropertyName)
		{
			m_sourcePropertyName = sourcePropertyName;
			m_targetPropertyName = targetPropertyName;
			m_filterPropertyName = filterPropertyName;
			m_pathPropertyName = pathPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_sourcePropertyName, out Vector3 source) &
				blackboard.TryGetStructValue(m_targetPropertyName, out Vector3 target) &
				blackboard.TryGetStructValue(m_filterPropertyName, out NavMeshQueryFilter filter))
			{
				if (!blackboard.TryGetClassValue(m_pathPropertyName, out NavMeshPath path) || path == null)
				{
					path = new NavMeshPath();
					blackboard.SetClassValue(m_pathPropertyName, path);
				}

				bool found = NavMesh.CalculatePath(source, target, filter, path);

				return StateToStatusHelper.ConditionToStatus(found);
			}

			return Status.Error;
		}
	}
}
