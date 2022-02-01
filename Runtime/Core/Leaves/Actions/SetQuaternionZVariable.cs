// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a <see cref="Quaternion.z"/> and sets a new quaternion into the <see cref="Blackboard"/>.
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
	/// 		<description>Property name of a quaternion of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of z of type <see cref="float"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class SetQuaternionZVariable : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_quaternionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_zPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName quaternionPropertyName, BlackboardPropertyName zPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(quaternionPropertyName, zPropertyName, resultPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string, string>.Setup(string quaternionPropertyName, string zPropertyName,
			string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(quaternionPropertyName),
				new BlackboardPropertyName(zPropertyName), new BlackboardPropertyName(resultPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName quaternionPropertyName, BlackboardPropertyName zPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_quaternionPropertyName = quaternionPropertyName;
			m_zPropertyName = zPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetStructValue(m_quaternionPropertyName, out Quaternion quaternion) &
				blackboard.TryGetStructValue(m_zPropertyName, out float z))
			{
				quaternion.z = z;
				blackboard.SetStructValue(m_resultPropertyName, quaternion);

				return Status.Success;
			}

			return Status.Error;
		}
	}
}
