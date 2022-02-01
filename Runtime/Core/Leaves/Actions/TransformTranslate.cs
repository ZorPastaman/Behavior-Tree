// Copyright (c) 2020-2022 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Behavior-Tree

using System.Runtime.CompilerServices;
using UnityEngine;
using Zor.BehaviorTree.DrawingAttributes;
using Zor.SimpleBlackboard.Core;

namespace Zor.BehaviorTree.Core.Leaves.Actions
{
	/// <summary>
	/// <para>
	/// Moves a <see cref="Transform"/> with <see cref="Transform.Translate(Vector3)"/>.
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
	/// 		<description>Translation of type <see cref="Vector3"/>.</description>
	/// 	</item>
	/// </list>
	/// </para>
	/// </summary>
	public sealed class TransformTranslate : Action,
		ISetupable<BlackboardPropertyName, Vector3>, ISetupable<string, Vector3>
	{
		[BehaviorInfo] private BlackboardPropertyName m_transformPropertyName;
		[BehaviorInfo] private Vector3 m_translation;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<BlackboardPropertyName, Vector3>.Setup(BlackboardPropertyName transformPropertyName,
			Vector3 translation)
		{
			SetupInternal(transformPropertyName, translation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void ISetupable<string, Vector3>.Setup(string transformPropertyName, Vector3 translation)
		{
			SetupInternal(new BlackboardPropertyName(transformPropertyName), translation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetupInternal(BlackboardPropertyName transformPropertyName, Vector3 translation)
		{
			m_transformPropertyName = transformPropertyName;
			m_translation = translation;
		}

		protected override Status Execute()
		{
			if (blackboard.TryGetClassValue(m_transformPropertyName, out Transform transform) & transform != null)
			{
				transform.Translate(m_translation);
				return Status.Success;
			}

			return Status.Error;
		}
	}
}
