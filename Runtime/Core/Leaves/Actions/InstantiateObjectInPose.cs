﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Instantiates an <see cref="Object"/> in a pose and sets it into the <see cref="Blackboard"/>.
	/// This <see cref="Action"/> uses <see cref="Object.Instantiate(Object, Vector3, Quaternion)"/>.
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
	/// 		<description>Property name of an object of type <see cref="Object"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a position of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a rotation of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name for a result of type <see cref="Object"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <remarks>
	/// The result is set into the <see cref="Blackboard"/> only if there's all the data and
	/// this <see cref="Action"/> ticks with <see cref="Status.Success"/>.
	/// </remarks>
	public sealed class InstantiateObjectInPose : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>,
		ISetupable<string, string, string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_objectPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_positionPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rotationPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_resultPropertyName;

		void ISetupable<BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName, BlackboardPropertyName>.
			Setup(BlackboardPropertyName objectPropertyName, BlackboardPropertyName positionPropertyName,
				BlackboardPropertyName rotationPropertyName, BlackboardPropertyName resultPropertyName)
		{
			SetupInternal(objectPropertyName, positionPropertyName, rotationPropertyName, resultPropertyName);
		}

		void ISetupable<string, string, string, string>.Setup(string objectPropertyName, string positionPropertyName,
			string rotationPropertyName, string resultPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(objectPropertyName),
				new BlackboardPropertyName(positionPropertyName), new BlackboardPropertyName(rotationPropertyName),
				new BlackboardPropertyName(resultPropertyName));
		}

		private void SetupInternal(BlackboardPropertyName objectPropertyName,
			BlackboardPropertyName positionPropertyName, BlackboardPropertyName rotationPropertyName,
			BlackboardPropertyName resultPropertyName)
		{
			m_objectPropertyName = objectPropertyName;
			m_positionPropertyName = positionPropertyName;
			m_rotationPropertyName = rotationPropertyName;
			m_resultPropertyName = resultPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_objectPropertyName, out Object @object) & @object != null &
				blackboard.TryGetStructValue(m_positionPropertyName, out Vector3 position) &
				blackboard.TryGetStructValue(m_rotationPropertyName, out Quaternion rotation))
			{
				blackboard.SetClassValue(m_resultPropertyName, Object.Instantiate(@object, position, rotation));
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
