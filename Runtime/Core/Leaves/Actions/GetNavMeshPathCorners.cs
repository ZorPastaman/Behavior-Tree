// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets a <see cref="NavMeshPath.corners"/> and sets it into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Returns in its tick:</term>
	/// 	</listheader>
	/// 	<item>
	/// 		<term><see cref="Status.Success"/> </term>
	/// 		<description>if there's all the data in the <see cref="Blackboard"/>.</description>
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
	/// 		<description>Property name of a nav mesh path of type <see cref="NavMeshPath"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of array of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
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
