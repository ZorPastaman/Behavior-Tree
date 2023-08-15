// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="Bounds"/> into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a center of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a size of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a bounds of type <see cref="Bounds"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SetBoundsVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_centerPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_sizePropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_boundsPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName centerPropertyName, BlackboardPropertyName sizePropertyName,
			BlackboardPropertyName boundsPropertyName)
		{
			SetupInternal(centerPropertyName, sizePropertyName, boundsPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string centerPropertyName, string sizePropertyName,
			string boundsPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(centerPropertyName), new BlackboardPropertyName(sizePropertyName),
				new BlackboardPropertyName(boundsPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName centerPropertyName, BlackboardPropertyName sizePropertyName,
			BlackboardPropertyName boundsPropertyName)
		{
			m_centerPropertyName = centerPropertyName;
			m_sizePropertyName = sizePropertyName;
			m_boundsPropertyName = boundsPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_centerPropertyName, out Vector3 center) &
				blackboard.TryGetStructValue(m_sizePropertyName, out Vector3 size))
			{
				blackboard.SetStructValue(m_boundsPropertyName, new Bounds(center, size));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
