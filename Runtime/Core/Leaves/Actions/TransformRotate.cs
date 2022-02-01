// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Rotates a <see cref="Transform"/> with <see cref="Transform.Rotate(Vector3)"/>.
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
	/// 		<description>Euler of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class TransformRotate : Action, 
		ISetupable<BlackboardPropertyName, Vector3>, ISetupable<string, Vector3>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private Vector3 m_euler;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, Vector3>.Setup(BlackboardPropertyName transformPropertyName, 
			Vector3 euler)
		{
			SetupInternal(transformPropertyName, euler);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, Vector3>.Setup(string transformPropertyName, Vector3 euler)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName), euler);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName, Vector3 euler)
		{
			m_transformPropertyName = transformPropertyName;
			m_euler = euler;
		}
		
		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null)
			{
				transform.Rotate(m_euler);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
