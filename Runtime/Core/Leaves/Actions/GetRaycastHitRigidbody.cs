// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Gets a <see cref="RaycastHit.rigidbody"/> and sets it into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a raycast hit of type <see cref="RaycastHit"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Rigidbody"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class GetRaycastHitRigidbody : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_raycastHitPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rigidbodyPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName raycastHitPropertyName, BlackboardPropertyName rigidbodyPropertyName)
		{
			SetupInternal(raycastHitPropertyName, rigidbodyPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string raycastHitPropertyName, string rigidbodyPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(raycastHitPropertyName),
				new BlackboardPropertyName(rigidbodyPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName raycastHitPropertyName,
			BlackboardPropertyName rigidbodyPropertyName)
		{
			m_raycastHitPropertyName = raycastHitPropertyName;
			m_rigidbodyPropertyName = rigidbodyPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_raycastHitPropertyName, out RaycastHit raycastHit))
			{
				blackboard.SetClassValue(m_rigidbodyPropertyName, raycastHit.rigidbody);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
