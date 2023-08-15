// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets a bounds of a collider and sets it into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a collider of type <see cref="Collider"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Bounds"/>.</description>
	/// 	</item>
	/// </list>
	/// The first argument in the setup method is a property name of a collider of type <see cref="Collider"/>.
	/// The second argument in the setup method is a property name for a result of type <see cref="Bounds"/>.
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class GetColliderBounds : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_colliderPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName colliderPropertyName, BlackboardPropertyName boundsPropertyName)
		{
			SetupInternal(colliderPropertyName, boundsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string colliderPropertyName, string boundsPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(colliderPropertyName),
				new BlackboardPropertyName(boundsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName colliderPropertyName,
			BlackboardPropertyName boundsPropertyName)
		{
			m_colliderPropertyName = colliderPropertyName;
			m_boundsPropertyName = boundsPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_colliderPropertyName, out Collider collider) & collider != null)
			{
				blackboard.SetStructValue(m_boundsPropertyName, collider.bounds);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
