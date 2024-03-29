﻿// Copyright (c) 2020-2023 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Sets a rotation to a <see cref="Transform"/>.
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
	/// 		<description>Property name of a transform of type <see cref="Transform"/>.</description>
	/// 	</item>
	/// 	<item>
	/// 		<description>Property name of a rotation of type <see cref="Quaternion"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class SetTransformRotation : Action,
		ISetupable<BlackboardPropertyName, BlackboardPropertyName>, ISetupable<string, string>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private BlackboardPropertyName m_rotationPropertyName;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, BlackboardPropertyName>.Setup(
			BlackboardPropertyName transformPropertyName, BlackboardPropertyName rotationPropertyName)
		{
			SetupInternal(transformPropertyName, rotationPropertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, string>.Setup(string transformPropertyName, string rotationPropertyName)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName),
				new BlackboardPropertyName(rotationPropertyName));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName,
			BlackboardPropertyName rotationPropertyName)
		{
			m_transformPropertyName = transformPropertyName;
			m_rotationPropertyName = rotationPropertyName;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null &
				blackboard.TryGetStructValue(m_rotationPropertyName, out Quaternion rotation))
			{
				transform.rotation = rotation;
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
