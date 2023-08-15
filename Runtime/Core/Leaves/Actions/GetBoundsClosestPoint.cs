// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Computes a closest point on a bounds to a point and sets it into the <see cref="Blackboard"/>.
	/// </para>
	/// <para>
	/// This <see cref="Action"/> uses <see cref="Bounds.ClosestPoint"/>.
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
	/// 		<description>Property name of a bounds of type <see cref="Bounds"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a point of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class GetBoundsClosestPoint : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_pointPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_closestPointPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName,
			BlackboardPropertyName closestPointPropertyName)
		{
			SetupInternal(boundsPropertyName, pointPropertyName, closestPointPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string boundsPropertyName, string pointPropertyName,
			string closestPointPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(boundsPropertyName), new BlackboardPropertyName(pointPropertyName),
				new BlackboardPropertyName(closestPointPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName boundsPropertyName, BlackboardPropertyName pointPropertyName,
			BlackboardPropertyName closestPointPropertyName)
		{
			m_boundsPropertyName = boundsPropertyName;
			m_pointPropertyName = pointPropertyName;
			m_closestPointPropertyName = closestPointPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_boundsPropertyName, out Bounds bounds) &
				blackboard.TryGetStructValue(m_pointPropertyName, out Vector3 point))
			{
				Vector3 closestPoint = bounds.ClosestPoint(point);
				blackboard.SetStructValue(m_closestPointPropertyName, closestPoint);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
